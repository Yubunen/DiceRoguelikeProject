using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //damage amplification
    [System.Serializable]
    public class Frozen : BaseCondition
    {
        [Header("Frozen")]
        [SerializeField] private float _amplifi;
        [SerializeField] private float _limit;
        private Buff nowBuff;

        public Frozen(int duration, int priority, float amplifi, float limit)
        {
            _duration = duration;
            _amplifi = amplifi;
            _limit = limit;
        }

        public Frozen(Frozen other)
        {
            _duration = other._duration;
            _amplifi = other._amplifi;
            _limit = other._limit;
        }

        public override void Activate(BaseUnit unit, List<BaseCondition> others)
        {
            var amplifi = _amplifi;
            foreach (var c in others)
            {
                if (c.GetType() == typeof(Wet)) amplifi *= 1.5f;
                if (c.GetType() == typeof(Oiled)) amplifi *= 0.75f;
            }

            amplifi *= _duration;
            amplifi = amplifi > _limit ? _limit : _amplifi;
            _duration--;

            if (nowBuff == null)
            {
                nowBuff = new Buff(_duration, new Ability(Status.zero, 0, 0, 0, amplifi, 0));
                unit.GetBuff(nowBuff);
            }
            else
            {
                nowBuff.ability.defenceRelative = amplifi;
                nowBuff.duration = _duration;
            }
        }
        public override void Interact(BaseCondition other)
        {
            if (other.GetType() == typeof(Burn))
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

            if (other.GetType() == typeof(Frozen))
            {
                Frozen temp = (Frozen)other;
                temp._duration += _duration;
                temp._limit = _limit > temp._limit ? _limit : temp._limit;
            }
        }
        public override void Add(BaseUnit unit)
        {
            nowBuff = new Buff(_duration, new Ability(Status.zero, 0, 0, 0, _amplifi, 0));
            unit.GetBuff(nowBuff);
        }
        public override void Remove(BaseUnit unit)
        {
            nowBuff.duration = 0;
        }
    }
}