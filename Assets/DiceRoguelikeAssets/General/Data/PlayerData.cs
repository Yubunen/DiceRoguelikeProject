using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Dice Roguelike/Data/Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public PlayerUnit playerPrefab;

        public Weapon weaponPrefab;

        public ArmParts armPartsPrefab;
        public BodyParts legPartsPrefab;
        public LegParts bodyPartsPrefab;

        public List<Accessory> accessoriePrefabs;

        public List<Consumable> consumablePrefabs;

        public List<Dice> dices;

        public List<ActionSkill> skills;
    }
}