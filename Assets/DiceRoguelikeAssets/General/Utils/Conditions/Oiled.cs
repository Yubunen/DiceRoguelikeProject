using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //Increase Burn, Poison  Decrease Frozen, Shock
    public class Oiled : BaseCondition
    {
        public override void Activate(BaseUnit unit)
        {
            _duration--;
        }

        public override void Interact(BaseCondition other)
        {

        }
    }

}