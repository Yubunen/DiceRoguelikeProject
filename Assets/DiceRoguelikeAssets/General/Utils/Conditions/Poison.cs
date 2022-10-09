using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //Damage per turn (stackable)
    [System.Serializable]
    public class Poison : BaseCondition
    {
        [Header("Poison")]
        [SerializeField] private int _damage;

        public Poison(int duration, int damage)
        {
            _duration = duration;
            _damage = damage;
        }

        public Poison(Poison other)
        {
            _duration = other._duration;
            _damage = other._damage;
        }

        public override void Activate(BaseUnit unit, List<BaseCondition> others)
        {
            float totalDamage = _damage;

            foreach (var c in others)
            {
                if (c.GetType() == typeof(Burn)) totalDamage *= 1.2f;
                if (c.GetType() == typeof(Frozen)) totalDamage *= 0.8f;
            }

            unit.GetEffect(new Effect(Effect.EffectType.Condition, new Status((int)totalDamage, 0)));
            _duration--;
        }

        public override void Interact(BaseCondition other)
        {
            if (other.GetType() != GetType()) return;
            Poison temp = (Poison)other;
            temp._duration = _duration > temp._duration ? _duration : temp._duration;
            temp._damage += _damage;
        }

        public override void Add(BaseUnit unit)
        {

        }
        public override void Remove(BaseUnit unit)
        {

        }
    }
}