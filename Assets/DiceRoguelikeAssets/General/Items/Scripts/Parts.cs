using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class Parts : BaseItem
    {
        [SerializeField] protected float _powerGen = 1f;
        [SerializeField] protected SubSkill _skillPrefab;

        protected SubSkill _skill;
        public SubSkill skill => _skill;
        public Parts(Parts other) : base(other.Info)
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
        public void PowerGenerate()
        {
            //foreach (var skill in skills)
            //{
            //    skill.SupplyPower(powerGen);
            //}
        }
    }
}