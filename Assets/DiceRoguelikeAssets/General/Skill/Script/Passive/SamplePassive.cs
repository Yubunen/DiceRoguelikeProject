using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

[CreateAssetMenu(fileName = "SamplePassive", menuName = "Dice Roguelike/Skill/Passive/SamplePassive", order = 0)]
public class SamplePassive : PassiveSkill
{
    protected override void Cast()
    {
        Debug.Log("sample passive skill");
    }
}
