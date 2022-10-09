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

        [SerializeField] private UnitResourceContainer _units;
        [SerializeField] private ItemResourceContainer _items;

        public static UnitResourceContainer units => instance._units;
        public static ItemResourceContainer items => instance._items;

        private void OnValidate()
        {
            instance = this;
        }
    }
}