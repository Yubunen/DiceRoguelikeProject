using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Item Resource Container", menuName = "Dice Roguelike/Resource/Item Resource Container", order = 1)]
    public class ItemResourceManager : ScriptableObject
    {
        [SerializeField] private ItemResourceContainer<Weapon> _weapons;
        [SerializeField] private ItemResourceContainer<ArmParts> _armParts;
        [SerializeField] private ItemResourceContainer<BodyParts> _bodyParts;
        [SerializeField] private ItemResourceContainer<LegParts> _legParts;
        [SerializeField] private ItemResourceContainer<Accessory> _accessories;
        [SerializeField] private ItemResourceContainer<Consumable> _consumables;


        public ItemResourceContainer<Weapon> Weapons => _weapons;
        public ItemResourceContainer<ArmParts> ArmParts => _armParts;
        public ItemResourceContainer<BodyParts> BodyParts => _bodyParts;
        public ItemResourceContainer<LegParts> LegParts => _legParts;
        public ItemResourceContainer<Accessory> Accessories => _accessories;
        public ItemResourceContainer<Consumable> Consumables => _consumables;

        private void OnValidate()
        {
            _weapons.Sort();
            _armParts.Sort();
            _bodyParts.Sort();
            _legParts.Sort();
            _accessories.Sort();
            _consumables.Sort();
        }
    }

    [System.Serializable]
    public class ItemResourceContainer<T> where T : BaseItem
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