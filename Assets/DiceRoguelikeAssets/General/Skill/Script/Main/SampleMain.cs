using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

[CreateAssetMenu(fileName = "SampleMain", menuName = "Dice Roguelike/Skill/Main/SampleMain", order = 0)]
public class SampleMain : MainSkill
{
    public override void Cast(BaseContainer target)
    {
        Debug.Log("sample main skill");
    }
}
