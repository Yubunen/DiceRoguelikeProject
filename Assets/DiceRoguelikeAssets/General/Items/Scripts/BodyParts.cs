using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Body Parts", menuName = "Dice Roguelike/Item/Body Parts", order = 0)]
    public class BodyParts : Parts
    {

        public BodyParts(BodyParts other): base(other)
        {

        }
    }
}