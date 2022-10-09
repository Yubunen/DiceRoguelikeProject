using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public struct UnitAction
    {
        public int cost;
        public MainSkill skill;

        public Sprite Icon => skill ? skill.Icon : null;

        public UnitAction(int cost, MainSkill skill)
        {
            this.cost = cost;
            this.skill = skill;
        }

        public void SetCost(int cost)
        {
            this.cost = cost;
        }

        public void SetSkill(MainSkill skill)
        {
            this.skill = skill;
        }
    }
}