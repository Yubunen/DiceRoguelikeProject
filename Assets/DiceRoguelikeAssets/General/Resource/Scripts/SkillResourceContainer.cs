using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "SkillResourceContainer", menuName = "DiceRogueLike/Resource/SkillResourceContainer", order = 1)]
    public class SkillResourceContainer : ScriptableObject
    {
        [SerializeField] private ResourceContainer<MainSkill> _mainSkills;
        [SerializeField] private ResourceContainer<SubSkill> _subSkills;
        [SerializeField] private ResourceContainer<PassiveSkill> _passiveSkills;

        public ResourceContainer<MainSkill> mainSkills => _mainSkills;
        public ResourceContainer<SubSkill> subSkills => _subSkills;
        public ResourceContainer<PassiveSkill> passiveSkills => _passiveSkills;
        private void OnValidate()
        {
            _mainSkills.Sort();
            _subSkills.Sort();
            _passiveSkills.Sort();
        }
    }
}