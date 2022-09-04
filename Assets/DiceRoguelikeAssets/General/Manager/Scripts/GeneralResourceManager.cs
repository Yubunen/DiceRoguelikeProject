using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace LSemiRoguelike
{
    [ExecuteAlways]
    public class GeneralResourceManager : MonoBehaviour
    {
        private static GeneralResourceManager instance = null;

        [SerializeField] private UnitResourceContainer _unit;
        [SerializeField] private WeaponResourceContainer _weapon;
        [SerializeField] private PartsResourceContainer _parts;
        [SerializeField] private AccessoryResourceContainer _accessory;
        [SerializeField] private SkillResourceContainer _skills;

        public static UnitResourceContainer unit => instance._unit;
        public static WeaponResourceContainer wepon => instance._weapon;
        public static PartsResourceContainer parts => instance._parts;
        public static AccessoryResourceContainer accessory => instance._accessory;
        public static SkillResourceContainer skills => instance._skills;

        private void OnValidate()
        {
            instance = this;
        }
    }
}