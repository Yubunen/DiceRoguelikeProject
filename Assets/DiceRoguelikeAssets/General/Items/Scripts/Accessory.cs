using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Accessory", menuName = "Dice Roguelike/Item/Accessory", order = 0)]
    public class Accessory : BaseItem
    {
        [SerializeField] private PassiveSkill _skillPrefab;
        private PassiveSkill _skill;
        public PassiveSkill Skill => _skill;

        public Accessory(Accessory other) : base(other.Info)
        {
            _skillPrefab = other._skillPrefab;
        }

        public override void Init()
        {
            _skill = Instantiate(_skillPrefab);
            _skill.Init(PlayerManager.Instance.Player);
        }

        public void Activate()
        {
            _skill?.Passive();
        }

        public override void Remove()
        {

        }
    }
}
