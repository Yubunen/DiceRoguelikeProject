using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LSemiRoguelike;

public class UnitStatusUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text hpTxt;
    [SerializeField] private Image shieldImg;
    [SerializeField] private Text shieldTxt;
    [SerializeField] private EffectPopupManager effectPopupManager;

    int maxHp, hp, shield;

    public virtual void InitUI(Status maxStatus, Status status)
    {
        maxHp = maxStatus.hp;
        hpSlider.maxValue = maxHp;

        hp = status.hp;
        hpSlider.value = hp;
        hpTxt.text = hp + "/" + maxHp;

        shield = status.shield;
        shieldTxt.text = shield.ToString();
        shieldImg.gameObject.SetActive(shield > 0);
    }

    public virtual void SetUI(Status status)
    {
        HpEffect(status.hp - hp);
        hp = status.hp;
        hpSlider.value = hp;
        hpTxt.text = hp + "/" + maxHp;


        ShieldEffect(status.shield - shield);
        shield = status.shield;
        shieldTxt.text = shield.ToString();
        shieldImg.gameObject.SetActive(shield > 0);
    }

    void HpEffect(int effect)
    {
        if (effect == 0) return;
        effectPopupManager.SetPopup(effect.ToString("N"), effect > 0 ? Color.green : Color.red);
    }

    void ShieldEffect(int effect)
    {
        if (effect == 0) return;
        effectPopupManager.SetPopup(effect.ToString("N"), effect > 0 ? Color.grey : Color.yellow);
    }

    public void SetCondition(List<BaseCondition> condition)
    {
        //TODO: condition ui
    }
}
