using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Leg Parts", menuName = "Dice Roguelike/Item/Leg Parts", order = 0)]
    public class LegParts : Parts
    {
        public LegParts(LegParts other): base(other)
        {

        }
    }
}