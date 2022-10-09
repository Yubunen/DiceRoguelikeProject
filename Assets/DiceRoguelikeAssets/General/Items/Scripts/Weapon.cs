using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class Weapon : BaseItem
    {
        [SerializeField] private MainSkill skillPrefab;
        private MainSkill _skill;
        [SerializeField] private int _cost;
        public MainSkill Skill => _skill;
        public int Cost => _cost;

        public Weapon(Weapon other)
        {
            _id = other._id;
            _name = other._name;
            _sprite = other._sprite;
            _ability = other._ability;
            skillPrefab = other.skillPrefab;
        }

        protected override void Init()
        {
            _skill = GameObject.Instantiate(skillPrefab, owner.transform);
            _skill.Init(owner);
        }
    }
}