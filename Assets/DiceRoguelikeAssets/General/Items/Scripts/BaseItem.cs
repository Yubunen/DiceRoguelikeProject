using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseItem : ScriptableObject
    {
        //info
        [SerializeField] private ItemInfo _info;
        public ItemInfo Info => _info;

        public BaseItem(ItemInfo info)
        {
            _info = info;
        }

        public abstract void Init();
        public abstract void Remove();
    }
}