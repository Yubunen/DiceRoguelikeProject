using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "BaseUnit", menuName = "Dice Roguelike/Unit/BaseUnit", order = 0)]
    [DisallowMultipleComponent]
    public class BaseUnit : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private UnitInfo _info;
        [SerializeField] protected Ability _ability;
        [SerializeField] private Animation _anim;

        public UnitInfo Info => _info;

        protected Status _status;
        protected Ability _totalAbility;
        protected BaseContainer _container;
        protected List<Buff> _buffs = new List<Buff>();

        public virtual bool IsDead => _status.hp <= 0;
        public Status TotalStatus => _status;
        public Status MaxStatus => TotalAbility.maxStatus;
        public Ability TotalAbility => _totalAbility;
        protected Conditions _conditions;
        public BaseContainer Container => _container;

        public void Init(BaseContainer container)
        {
            _container = container;
            Init();
        }

        protected virtual void Init()
        {
            SetAbility();
            _status.hp = MaxStatus.hp;
            _conditions = new Conditions();
            _conditions.Init(this);
        }


        public void Activate()
        {
            _buffs.ForEach((b) => {
                b.duration--;
                if (b.duration <= 0) _buffs.Remove(b);
            });
            _conditions.Activate();

            _status.power += MaxStatus.power;
            _status.power = _status.power > MaxStatus.power? MaxStatus.power : _status.power;
        }

        protected virtual void SetAbility()
        {
            _totalAbility = _ability;
            foreach (Buff buff in _buffs) _totalAbility += buff.ability;
        }

        public virtual Effect SetEffect(Effect effect)
        {
            effect.status.hp += (int)(effect.status.hp * TotalAbility.attackRelative);
            if (effect.status.hp > 0)
                effect.status.hp += (int)TotalAbility.attackAbsolute;
            else if (effect.status.hp < 0)
                effect.status.hp -= (int)(TotalAbility.attackAbsolute);

            return effect;
        }

        public virtual void GetCondition(BaseCondition condition)
        {
            _conditions.Add(condition);
        }

        public virtual void GetBuff(Buff buff)
        {
            _buffs.Add(buff);
        }

        public virtual void GetEffect(Effect effect)
        {
            Effect finalEffect = new Effect();
            //Status
            {
                var beforeSts = _status;
                if (_status.shield != 0)
                {
                    _status.shield += effect.status.shield;
                    _status.shield = _status.shield > MaxStatus.shield ? MaxStatus.shield : _status.shield;
                    _status.shield = _status.shield < 0 ? 0 : _status.shield;
                }

                if (effect.status.hp < 0)
                {
                    if (effect.effectType == Effect.EffectType.Condition)
                    {
                        _status.hp += effect.status.hp;
                    }
                    else
                    {
                        var dmg = (int)((effect.status.hp + TotalAbility.defenceAbsolute) * TotalAbility.defenceRelative);
                        _status.shield += dmg >= 0 ? dmg : -1;
                        if (_status.shield < 0)
                        {
                            _status.hp += _status.shield;
                            _status.shield = 0;
                        }
                    }
                }
                else if (effect.status.hp > 0)
                {
                    _status.hp += effect.status.hp;
                    if (_status.hp > MaxStatus.hp)
                        _status.hp = MaxStatus.hp;
                }
                finalEffect.status = beforeSts - _status;
            }

            //Condition, Buff
            if(effect.conditions != null)
            {
                foreach (var condition in effect.conditions)
                {
                    GetCondition(condition);
                }
                foreach (var buff in effect.buffs)
                {
                    GetBuff(buff);
                }
            }
            Container.UpdateStatusUI();
        }
    }
}