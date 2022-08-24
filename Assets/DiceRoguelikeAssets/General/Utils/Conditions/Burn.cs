using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //damage per turn (unstackable)
    [System.Serializable]
    public class Burn : BaseCondition
    {

        [Header("Burn")]
        [SerializeField] private int _damage;

        public Burn(int duration, int priority, int damage)
        {
            _duration = duration;
            _damage = damage;
        }

        public Burn(Burn other)
        {
            _duration = other._duration;
            _damage = other._damage;
        }

        public override void Activate(BaseUnit unit, List<BaseCondition> others)
        {
            float totalDamage = _damage;

            foreach (var c in others)
            {
                if (c.GetType() == typeof(Wet)) totalDamage *= 0.5f;
                if (c.GetType() == typeof(Oiled)) totalDamage *= 2f;
            }
            unit.GetEffect(new Effect(Effect.EffectType.Condition, new Status(_damage, 0)));
            _duration--;
        }

        public override void Interact(BaseCondition other)
        {
            if (other.GetType() == typeof(Frozen))
            {
                if (other._duration > _duration)
                {
                    other._duration -= _duration;
                    _duration = 0;
                    return;
                }
                else if (other._duration > _duration)
                {
                    _duration -= _duration;
                    other._duration = 0;
                }
                else
                {
                    other._duration = 0;
                    _duration = 0;
                }
            }

            if (other.GetType() != GetType()) return;
            other._duration = _duration > other._duration ? _duration : other._duration;
            ((Burn)other)._damage = _damage > ((Burn)other)._damage ? _damage : ((Burn)other)._damage;
        }

        public override void Add(BaseUnit unit)
        {

        }
        public override void Remove(BaseUnit unit)
        {

        }
    }
}