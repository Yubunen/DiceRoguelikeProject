using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Shooting
{
    public class ShootingSkillUnit : ShootingActerUnit
    {
        public new SkillUnit Unit => base.Unit as SkillUnit;

        protected override void MoveUpdate()
        {

        }
    }
}