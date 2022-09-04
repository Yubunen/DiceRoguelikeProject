using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    //Damage per turn (stackable)
    public class Poison : BaseCondition
    {
        [Header("Poison")]
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