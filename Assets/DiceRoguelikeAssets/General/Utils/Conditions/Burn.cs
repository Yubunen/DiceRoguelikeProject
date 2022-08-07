using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //damage per turn (unstackable)
    public class Burn : BaseCondition
    {
        [Header("Burn")]
        private float _damage;
        public override void Activate(BaseUnit unit)
        {
            unit.GetEffect(new Effect(Effect.EffectType.Condition, new Status(_damage, 0)));
            _duration--;
        }

        public override void Interact(BaseCondition other)
        {

        }
    }

}