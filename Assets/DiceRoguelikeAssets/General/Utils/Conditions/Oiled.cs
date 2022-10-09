using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //Increase Burn, Poison  Decrease Frozen, Shock
    [System.Serializable]
    public class Oiled : BaseCondition
    {
        public Oiled(int duration)
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