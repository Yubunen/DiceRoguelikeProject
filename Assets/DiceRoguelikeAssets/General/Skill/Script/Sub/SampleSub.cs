using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

[CreateAssetMenu(fileName = "SampleSub", menuName = "Dice Roguelike/Skill/Sub/SampleSub", order = 0)]
public class SampleSub : SubSkill
{
    protected override void Cast()
    {
        Debug.Log("sample passive skill");
    }
}
