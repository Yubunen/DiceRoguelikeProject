using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

[CreateAssetMenu(fileName = "SelfEffect", menuName = "Dice Roguelike/Skill/Passive/SelfEffect", order = 0)]
public class SelfEffect : PassiveSkill
{
    [SerializeField] Effect _effect;
    protected override void Cast()
    {
        Debug.Log(Caster);
        Debug.Log(Container);
        Container.GetEffect(_effect);
    }
}
