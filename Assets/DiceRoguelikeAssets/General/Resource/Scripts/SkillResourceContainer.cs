using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Skill Resource Container", menuName = "Dice Roguelike/Resource/Skill Resource Container", order = 1)]
    public class SkillResourceContainer : ScriptableObject
    {
        [SerializeField] private SkillResourceContainer<ActionSkill> _actionSkills;
        [SerializeField] private SkillResourceContainer<SubSkill> _subSkills;
        [SerializeField] private SkillResourceContainer<PassiveSkill> _passiveSkills;

        public SkillResourceContainer<ActionSkill> MainSkills => _actionSkills;
        public SkillResourceContainer<SubSkill> SubSkills => _subSkills;
        public SkillResourceContainer<PassiveSkill> PassiveSkills => _passiveSkills;
        private void OnValidate()
        {
            _actionSkills.Sort();
            _subSkills.Sort();
            _passiveSkills.Sort();
        }
    }

    [System.Serializable]
    public class SkillResourceContainer<T> where T : BaseSkill
    {
        [SerializeField] private List<T> resources = new List<T>();

        public string[] GetItemIDs()
        {
            string[] ids = new string[resources.Count];
            for (int i = 0; i < ids.Length; i++) ids[i] = resources[i].Info.ID.ToString();
            return ids;
        }
        public string[] GetItemNames()
        {
            string[] ids = new string[resources.Count];
            for (int i = 0; i < ids.Length; i++) ids[i] = resources[i].Info.Name.ToString();
            return ids;
        }

        public void Sort()
        {
            resources.Sort((a, b) => a.Info.ID - b.Info.ID);
        }

        public T GetByID(int id)
        {
            if (resources?.Count == 0) return default(T);
            return resources.Find((x) => {
                return x.Info.ID == id;
            });
        }

        public T GetByName(string name)
        {
            if (resources?.Count == 0) return default(T);
            return resources.Find((x) => {
                return x.Info.Name == name;
            });
        }
    }
}