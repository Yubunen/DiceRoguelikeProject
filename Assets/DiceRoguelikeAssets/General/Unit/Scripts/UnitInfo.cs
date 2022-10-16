using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class UnitInfo
    {
        public enum UnitType { ObjectUnit, SkillUnit, PlayerUnit }
        [SerializeField] private int _id;
        [SerializeField] private UnitType _type;
        [SerializeField] private string _name;
        [SerializeField] private string _tooltip;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private BaseUnit _prefab;

        public int ID=> _id;
        public UnitType Type => _type;
        public string Name=> _name;
        public string Tooltip=> _tooltip;
        public Sprite Sprite => _sprite;
        public BaseUnit Prefab => _prefab;
    }
}