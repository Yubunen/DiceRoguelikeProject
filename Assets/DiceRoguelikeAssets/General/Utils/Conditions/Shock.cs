using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //stun
    [System.Serializable]
    public class Shock : BaseCondition
    {
        [SerializeField] private float _power;

        public Shock(int duration, float power)
        {
            _duration = duration;
            _power = power;
        }

        public Shock(Shock other)
        {
            _duration = other._duration;
            _power = other._power;
        }

        public override void Activate(BaseUnit unit, List<BaseCondition> others)
        {
            float threshold = unit.TotalStatus.power;

            foreach (var c in others)
            {
                if (others.GetType() == typeof(Burn)) threshold *= 1.5f;
                if (others.GetType() == typeof(Frozen)) threshold *= 0.75f;

                if (others.GetType() == typeof(Wet)) _power += 0.5f;
                if (others.GetType() == typeof(Oiled)) _power -= 0.5f;
            }

            if (_power > threshold)
            {
                unit.GetBuff(new Buff(1, new Ability(new Status(0, 0, -unit.TotalStatus.power))));
            }

            _duration--;
        }

        public override void Interact(BaseCondition other)
        {
            if (other.GetType() == typeof(Shock))
            {
                Shock temp = ((Shock)other);
                temp._power += _power;
                temp._duration = _duration > temp._duration ? _duration : temp._duration;
            }
        }

        public override void Add(BaseUnit unit)
        {

        }

        public override void Remove(BaseUnit unit)
        {

        }
    }
}