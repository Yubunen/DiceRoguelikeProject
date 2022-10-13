using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "Dice Roguelike/Item/Consumable", order = 0)]
    public class Consumable : BaseItem
    {
        [SerializeField] Effect _effect;

        public Consumable(Consumable other) : base(other.Info)
        {
            _effect = other._effect;
        }

        public override void Init()
        {

        }

        public override void Remove()
        {

        }
    }
}