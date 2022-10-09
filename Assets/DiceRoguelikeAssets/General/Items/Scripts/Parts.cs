using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class Parts : BaseItem
    {
        [SerializeField] protected float _powerGen = 1f;
        [SerializeField] protected SubSkill skillPrefab;

        protected SubSkill _skill;
        public SubSkill skill => _skill;

        public Parts(Parts other)
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

        public void PowerGenerate()
        {
            //foreach (var skill in skills)
            //{
            //    skill.SupplyPower(powerGen);
            //}
        }
    }
}