using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Shooting
{
    public class Magazine
    {
        List<Ammunition> ammunitions = new List<Ammunition>();
        int nowAmmunition = 0;

        public void ChangeAmmunition(int change)
        {
            nowAmmunition += change;
            nowAmmunition = nowAmmunition < 0 ? nowAmmunition+ammunitions.Count : nowAmmunition;
            nowAmmunition %= nowAmmunition + ammunitions.Count;
        }

        public MainSkill GetSkill()
        {
            return ammunitions[nowAmmunition].skill;
        }
    }
}