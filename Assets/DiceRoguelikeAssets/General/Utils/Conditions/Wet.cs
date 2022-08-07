using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //Increase Frozen, Shock    Decrease Burn, Poison
    public class Wet : BaseCondition
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