using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectPopupUI : MonoBehaviour
{
    private EffectPopupManager manager;
    private TextMeshProUGUI popupText;

    public void Init(EffectPopupManager manager)
    {
        this.manager = manager;
        popupText = GetComponent<TextMeshProUGUI>();
    }

    public void SetPopup(string text, Color color)
    {
        gameObject.SetActive(true);
        popupText.text = text;
        popupText.faceColor = color;
    }

    public void PopupEnd()
    {
        manager.BackToPool(this);
        gameObject.SetActive(false);
    }
}
