using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LSemiRoguelike;

public class UnitProgressUI : MonoBehaviour
{
    private Image image;
    private float progress;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Init(ActingUnit unit)
    {
        image.sprite = unit.sprite;
        SetProgress(100);
    }

    public void SetProgress(float progress)
    {
        this.progress = progress;
        transform.localPosition = ProgressUIManager.instance.GetPosByProgress(progress);
    }
}
