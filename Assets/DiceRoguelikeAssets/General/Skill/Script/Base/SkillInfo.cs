using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class SkillInfo
    {
        public enum SkillType { Main, Special, Sub, Passive }
        [SerializeField] private int _id;
        [SerializeField] private SkillType _type;
        [SerializeField] private string _name;
        [SerializeField] private string _tooltip;
        [SerializeField] private int _grade = 0;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private BaseSkill _prefab;

        public int ID => _id;
        public SkillType Type => _type;
        public string Name => _name;
        public string Tooltip => _tooltip;
        public Sprite Sprite => _sprite;
        public int grade => _grade;
        public BaseSkill Prefab => _prefab;
    }
}