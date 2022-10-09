using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class Consumable : IHaveInfo
    {
        [SerializeField] int _id;
        [SerializeField] string _name;
        [SerializeField] string _description;
        [SerializeField] Sprite _icon;

        [SerializeField] Effect _effect;

        public int ID => _id;

        public string Name => _name;

        public string Description => _description;

        public Sprite Icon => _icon;
    }
}