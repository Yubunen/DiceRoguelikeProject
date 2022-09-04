using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //Decrease Energy, Energy:0 == stun
    public class Shock : BaseCondition
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