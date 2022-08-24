using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //Increase Frozen, Shock    Decrease Burn, Poison
    [System.Serializable]
    public class Wet : BaseCondition
    {
        public Wet(int duration)
        {
            _duration = duration;
        }

        public override void Activate(BaseUnit unit, List<BaseCondition> others)
        {
            _duration--;
        }

        public override void Interact(BaseCondition other)
        {
            if (other.GetType() != GetType()) return;
        }

        public override void Add(BaseUnit unit)
        {

        }
        public override void Remove(BaseUnit unit)
        {

        }
    }
}