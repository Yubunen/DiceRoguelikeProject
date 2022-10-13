using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

[CreateAssetMenu(fileName = "Direct Effect", menuName = "Dice Roguelike/Skill/Main/Direct Effect", order = 0)]
public class DirectEffect : MainSkill
{
    [SerializeField] private Effect effect;
    [SerializeField] private GameObject vfxPrefab;
    [SerializeField] private AudioClip sfxClip;


    public override void Cast(BaseContainer target)
    {
        var finalEffect = effect;
        finalEffect = Caster.SetEffect(finalEffect);
        if(vfxPrefab) Instantiate(vfxPrefab, target.Pos, vfxPrefab.transform.rotation);
        target.GetEffect(finalEffect);
    }
}
