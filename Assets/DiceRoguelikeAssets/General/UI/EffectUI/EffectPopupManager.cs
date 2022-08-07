using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPopupManager : MonoBehaviour
{
    [SerializeField] private EffectPopupUI prefab;
    [SerializeField] private int initPoolCount;
    private Queue<EffectPopupUI> pool;

    private void Awake()
    {
        pool = new Queue<EffectPopupUI>();
        for (int i = 0; i < initPoolCount; i++)
        {
            AddPool();
        }
    }

    private void AddPool()
    {
        var obj = Instantiate(prefab, transform);
        obj.Init(this);
        pool.Enqueue(obj);
    }

    public void SetPopup(string text, Color color)
    {
        if (pool.Count == 0) AddPool();
        pool.Dequeue().SetPopup(text, color);
    }

    public void BackToPool(EffectPopupUI instance)
    {
        pool.Enqueue(instance);
    }
}
