using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Effect
    {
        public enum EffectType { Main, Sub, Passive, Condition }

        public EffectType effectType;

        public Status status;
        //hp : heal / damage
        //shield : charge/ break

        //conditions
        public BaseCondition[] conditions;

        public Vector3 knockback;
        public Effect(EffectType effectType, Status status) : this(effectType, status, null, new Vector3()) { }
        public Effect(EffectType effectType, Status status, BaseCondition[] conditions, Vector3 knockback)
        {
            this.effectType = effectType;
            this.status = status;
            this.conditions = conditions;
            this.knockback = knockback;
        }
    }
}