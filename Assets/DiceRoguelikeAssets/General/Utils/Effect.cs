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

        //hp : heal / damage
        //shield : charge/ break
        public Status status;

        //conditions
        public BaseCondition[] conditions;
        public Buff[] buffs;
        public Vector3 knockback;
        public Effect(EffectType effectType, Status status) : this(effectType, status, null, null, Vector3.zero) { }
        public Effect(EffectType effectType, Status status, BaseCondition[] conditions) : this(effectType, status, conditions, null, Vector3.zero) { }
        public Effect(EffectType effectType, Status status, Buff[] buffs) : this(effectType, status, null, buffs, Vector3.zero) { }
        public Effect(EffectType effectType, Status status, BaseCondition[] conditions, Buff[] buffs, Vector3 knockback)
        {
            this.effectType = effectType;
            this.status = status;
            this.conditions = conditions;
            this.buffs = buffs;
            this.knockback = knockback;
        }
    }
}