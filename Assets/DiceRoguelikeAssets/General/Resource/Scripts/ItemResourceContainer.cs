using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "ItemResourceContainer", menuName = "DiceRogueLike/Resource/ItemResourceContainer", order = 1)]
    public class ItemResourceContainer : ScriptableObject
    {
        [SerializeField] private ResourceContainer<Weapon> _weapons;
        [SerializeField] private ResourceContainer<Parts> _armParts, _bodyParts, _legParts;
        [SerializeField] private ResourceContainer<Accessory> _accessories;
        [SerializeField] private ResourceContainer<Consumable> _consumables;


        public ResourceContainer<Weapon> Weapons => _weapons;
        public ResourceContainer<Parts> ArmParts => _armParts;
        public ResourceContainer<Parts> BodyParts => _bodyParts;
        public ResourceContainer<Parts> LegParts => _legParts;
        public ResourceContainer<Accessory> accessories => _accessories;
        public ResourceContainer<Consumable> consumables => _consumables;

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
}