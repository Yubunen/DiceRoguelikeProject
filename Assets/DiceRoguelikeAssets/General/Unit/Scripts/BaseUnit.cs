using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LSemiRoguelike
{
    [DisallowMultipleComponent]
    public class BaseUnit : MonoBehaviour, IHaveInfo
    {
        [Header("Info")]
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        [SerializeField] protected Sprite _sprite;
        public uint ID => _id;
        public string Name => _name;

        [Header("Status")]
        [SerializeField] protected Status _maxStatus;
        [SerializeField] protected Ability _initAbility;
        [SerializeField] protected BaseCondition[] _initCondition;

        protected SpriteRenderer _renderer;
        public Sprite sprite => _sprite;


        protected Status _status;
        protected Ability _ability;
        protected List<BaseCondition> _conditions = new List<BaseCondition>();
        protected BaseContainer _container;

        public Status TotalStatus => _status;
        public Status MaxStatus => _maxStatus;
        public Ability TotalAbility => _ability;
        public List<BaseCondition> TotalCondition => _conditions;
        public BaseContainer Container => _container;


        public void Init(BaseContainer container)
        {
            _container = container;
            Init();
        }

        protected virtual void Init()
        {
            _status.hp = _maxStatus.hp;
            SetAbility();
            _conditions.AddRange(_initCondition);
        }

        public virtual bool IsDead => _status.hp <= 0;

        public virtual void SetAbility()
        {
            _ability = _initAbility;
        }

        public virtual Effect SetEffect(Effect effect)
        {
            effect.status.hp += effect.status.hp * _ability.attackMulti;
            if (effect.status.hp > 0)
                effect.status.hp += _ability.attackIncrese;
            else if (effect.status.hp < 0)
                effect.status.hp -= _ability.attackIncrese;

            return effect;
        }

        public virtual void GetEffect(Effect effect)
        {
            Effect finalEffect = new Effect();
            //status
            {
                var beforeSts = _status;
                _status.shield += effect.status.shield;
                _status.shield = _status.shield > MaxStatus.shield ? MaxStatus.shield : _status.shield;
                _status.shield = _status.shield < 0 ? 0 : _status.shield;

                if (effect.status.hp < 0)
                {
                    var dmg = (effect.status.hp + _ability.damageReduce) * _ability.damageMulti;
                    _status.shield += dmg < 0 ? dmg : -1;
                    if (_status.shield < 0)
                    {
                        _status.hp += _status.shield;
                        _status.shield = 0;
                    }
                }
                else if (effect.status.hp > 0)
                {
                    _status.hp += effect.status.hp;
                    if (_status.hp > _maxStatus.hp)
                        _status.hp = _maxStatus.hp;
                }
                finalEffect.status = beforeSts - _status;
            }
            //TODO: Condition
            {

            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_renderer && _sprite)
            {
                _renderer.sprite = _sprite;
            }
        }

        protected static void CreateUnit(MenuCommand menuCommand, System.Type type)
        {
            var obj = new GameObject("BaseUnit", type);

            var renderer = new GameObject("Renderer");
            renderer.transform.SetParent(obj.transform);
            var spriteRender = renderer.AddComponent<SpriteRenderer>();
            spriteRender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            spriteRender.receiveShadows = true;
            spriteRender.material = MaterialManager.UnitMaterial;

            obj.GetComponent<BaseUnit>()._renderer = renderer.GetComponent<SpriteRenderer>();

            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(obj, "Create" + obj.name);

            Selection.activeObject = obj;
        }

        [MenuItem("GameObject/Dice Rogue Like/Base Unit", false, 10)]
        static void CreateBaseUnit(MenuCommand menuCommand) { CreateUnit(menuCommand, typeof(BaseUnit)); }
#endif
    }
}