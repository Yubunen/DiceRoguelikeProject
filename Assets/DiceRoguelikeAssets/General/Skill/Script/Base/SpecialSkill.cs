using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Special Skill", menuName = "Dice Roguelike/Skill/Special Skill", order = 0)]
    public class SpecialSkill : ActionSkill
    {
        [SerializeField] int specialValue;
    }
}