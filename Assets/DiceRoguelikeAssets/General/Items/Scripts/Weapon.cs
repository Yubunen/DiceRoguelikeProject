using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Dice Roguelike/Item/Weapon", order = 0)]
    public class Weapon : BaseItem
    {
        [SerializeField] private MainSkill _skillPrefab;
        [SerializeField] private int _cost;
        private MainSkill _skill;
        public MainSkill Skill => _skill;
        public int Cost => _cost;

        public Weapon(Weapon other) : base(other.Info)
        {
            _skillPrefab = other._skillPrefab;
        }

        public override void Init()
        {
            _skill = Instantiate(_skillPrefab);
            _skill.Init(PlayerManager.Instance.Player);
        }

        public override void Remove()
        {

        }
    }
}