using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [System.Serializable]
    public class ResourceContainer<T> where T : IHaveInfo
    {
        [SerializeField] private List<T> resources = new List<T>();

        private void OnValidate()
        {
            resources.Sort((a, b) =>
            {
                return (int)(a.ID - b.ID);
            });
        }

        public T GetByID(uint id)
        {
            return resources.Find((x) => { return x.ID == id; });
        }
    }
}