using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LSemiRoguelike;

public class InfoUI : MonoBehaviour
{
    private IHaveInfo _info;
    [SerializeField] private Image iconUI;
    [SerializeField] private Text nameTxtUI, descriptionTxtUI;

    public IHaveInfo Info => _info;

    public void SetInfo(IHaveInfo info)
    {
        _info = info;
        iconUI.sprite = _info.Icon;
        nameTxtUI.text = _info.Name;
        descriptionTxtUI.text = _info.Description;
    }
}
