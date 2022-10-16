using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Unit Resource Container", menuName = "Dice Roguelike/Resource/Unit Resource Container", order = 1)]
    public class UnitResourceManager : ScriptableObject
    {
        [SerializeField] private UnitResourceContainer<BaseUnit> _baseUnits;
        [SerializeField] private UnitResourceContainer<SkillUnit> _skillUnits;
        [SerializeField] private UnitResourceContainer<PlayerUnit> _playerUnits;

        public UnitResourceContainer<BaseUnit> BaseUnits => _baseUnits;
        public UnitResourceContainer<SkillUnit> SkillUnits => _skillUnits;
        public UnitResourceContainer<PlayerUnit> PlayerUnits => _playerUnits;

        private void OnValidate()
        {
            _baseUnits.Sort();
            _skillUnits.Sort();
            _playerUnits.Sort();
        }

        public BaseUnit GetByID(int id)
        {
            BaseUnit unit = null;
            unit = _baseUnits.GetByID(id);
            if (!unit) unit = _skillUnits.GetByID(id);
            if (!unit) unit = _playerUnits.GetByID(id);
            return unit;
        }
    }

    [System.Serializable]
    public class UnitResourceContainer<T> where T : BaseUnit
    {
        [SerializeField] private List<T> resources = new List<T>();

        public string[] GetItemIDs()
        {
            string[] ids = new string[resources.Count];
            for(int i = 0; i < ids.Length; i++) ids[i] = resources[i].Info.ID.ToString();
            return ids;
        }
        public string[] GetItemNames()
        {
            string[] ids = new string[resources.Count];
            for(int i = 0; i < ids.Length; i++) ids[i] = resources[i].Info.Name.ToString();
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