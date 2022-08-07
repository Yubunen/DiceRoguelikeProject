using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Ability
    {
        //attack
        public float attackIncrese;
        public float attackMulti;

        //damaged
        public float damageReduce;
        public float damageMulti;

        public float speed;

        List<BaseCondition> attackCon;

        public Ability(float attackIncrese, float attackMulti, float damageReduce, float damageMulti, float speed) : this(attackIncrese, attackMulti, damageReduce, damageMulti, speed, null) { }
        public Ability(float attackIncrese, float attackMulti, float damageReduce, float damageMulti, float speed, List<BaseCondition> attackCon)
        {
            this.attackIncrese = attackIncrese; 
            this.attackMulti = attackMulti;
            this.damageReduce = damageReduce;
            this.damageMulti = damageMulti;
            this.speed = speed;

            this.attackCon = attackCon;
        }

        public override string ToString()
        {
            return
                "Attack Increase: " + attackIncrese + "\tAttack Multi: " + attackMulti +
                "\nDamage Reduce: " + damageReduce +"\tDamage Multi " + damageMulti +
                "\nSpeed: " + speed;
        }
    }
}