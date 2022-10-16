using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class ItemInfo
    {
        public enum ItemType { Weapon, ArmParts, BodyParts, LegParts, Accessory, Consumable }

        [SerializeField] private int _id;
        [SerializeField] private ItemType _type;
        [SerializeField] private string _name;
        [SerializeField] private string _tooltip;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private BaseItem _prefab;

        public int ID => _id;
        public string Name => _name;
        public string Tooltip => _tooltip;
        public Sprite Sprite => _sprite;
        public BaseItem Prefab => _prefab;
    }
}