using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "ItemResourceContainer", menuName = "DiceRogueLike/Resource/ItemResourceContainer", order = 1)]
    public class ItemResourceContainer : ScriptableObject
    {
        [SerializeField] private ResourceContainer<Weapon> _weapons;
        [SerializeField] private ResourceContainer<Parts> _parts;
        [SerializeField] private ResourceContainer<Accessory> _accessories;


        public ResourceContainer<Weapon> weapons => _weapons;
        public ResourceContainer<Parts> parts => _parts;
        public ResourceContainer<Accessory> accessories => _accessories;
    }
}