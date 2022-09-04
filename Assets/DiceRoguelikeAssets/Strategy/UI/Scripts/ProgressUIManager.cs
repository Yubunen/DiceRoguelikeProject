using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSemiRoguelike;

public class ProgressUIManager : MonoBehaviour
{
    public static ProgressUIManager instance;
    [SerializeField] private UnitProgressUI progressUiPrefab;
    public RectTransform rectTransform => base.transform as RectTransform;
    private void Awake()
    {
        instance = this;
    }


    public UnitProgressUI InstantiateUnitProgressUI(ActingUnit unit)
    {
        var ui = Instantiate(progressUiPrefab, transform);
        ui.Init(unit);
        return ui;
    }

    public Vector3 GetPosByProgress(float progress)
    {
        return new Vector3(-rectTransform.rect.width * (progress / 100.0f - 0.5f), 0, 0);
    }
}
