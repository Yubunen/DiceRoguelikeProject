using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Shooting
{
    public class ShootingDiceManager : DiceManager
    {
        protected override void Init()
        {

        }

        public override void GetActions(int power)
        {
            returnAction(new List<MainSkill>() { weaponSkill });
        }
    }
}