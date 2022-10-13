using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Arm Parts", menuName = "Dice Roguelike/Item/Arm Parts", order = 0)]
    public class ArmParts : Parts
    {


        public ArmParts(Parts other): base(other)
        {

        }
    }
}