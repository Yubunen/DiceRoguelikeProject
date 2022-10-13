using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class PassiveSkill : BaseSkill
    {
        [SerializeField] private int _cost;
        private int count;

        public void Passive()
        {
            count++;
            if (count >= _cost)
            {
                Cast();
                count -= _cost;
            }
        }

        protected abstract void Cast();
    }
}