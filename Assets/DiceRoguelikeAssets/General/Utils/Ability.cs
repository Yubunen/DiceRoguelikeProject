using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Ability
    {
        //status
        public Status maxStatus;

        //attack
        public int attackAbsolute;
        public float attackRelative;

        //damaged
        public int defenceAbsolute;
        public float defenceRelative;

        public float speed;


        public Ability(Status maxStatus) : this(maxStatus, 0, 1, 0, 1, 0) { }
        public Ability(Status maxStatus, int attackAbsolute, float attackRelative, int defenceAbsolute, float defenceRelative, float speed)
        {
            this.maxStatus = maxStatus;
            this.attackAbsolute = attackAbsolute; 
            this.attackRelative = attackRelative;
            this.defenceAbsolute = defenceAbsolute;
            this.defenceRelative = defenceRelative;
            this.speed = speed;
        }

        public static Ability operator +(Ability a, Ability b)
        {
            return new Ability(
                a.maxStatus + b.maxStatus, 
                a.attackAbsolute + b.attackAbsolute, 
                a.attackRelative + b.attackRelative, 
                a.defenceAbsolute + b.defenceAbsolute, 
                a.defenceRelative + b.attackAbsolute, 
                a.speed + b.speed
                );
        }

        public override string ToString()
        {
            return
                "Attack Increase: " + attackAbsolute + "\tAttack Multi: " + attackRelative +
                "\nDamage Reduce: " + defenceAbsolute +"\tDamage Multi " + defenceRelative +
                "\nSpeed: " + speed;
        }
    }
}