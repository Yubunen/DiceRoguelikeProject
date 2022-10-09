using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [System.Serializable]
    public class ResourceContainer<T> where T : IHaveInfo
    {
        [SerializeField] private List<T> resources = new List<T>();

        public string[] GetItemIDs()
        {
            string[] ids = new string[resources.Count];
            for(int i = 0; i < ids.Length; i++) ids[i] = resources[i].ID.ToString();
            return ids;
        }
        public string[] GetItemNames()
        {
            string[] ids = new string[resources.Count];
            for(int i = 0; i < ids.Length; i++) ids[i] = resources[i].Name.ToString();
            return ids;
        }

        public void Sort()
        {
            resources.Sort((a, b) =>
            {
                return (a.ID - b.ID);
            });
        }

        public T GetByID(int id)
        {
            if (resources?.Count == 0) return default(T);
            return resources.Find((x) => {
                return x.ID == id; 
            });
        }

        public T GetByName(string name)
        {
            if (resources?.Count == 0) return default(T);
            return resources.Find((x) => {
                return x.Name == name;
            });
        }
    }
}