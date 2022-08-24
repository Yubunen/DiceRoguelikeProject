using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    public class Buff
    {
        public int duration;
        public Ability ability;

        public Buff(int duration, Ability ability)
        {
            this.duration = duration;
            this.ability = ability;
        }
    }
}