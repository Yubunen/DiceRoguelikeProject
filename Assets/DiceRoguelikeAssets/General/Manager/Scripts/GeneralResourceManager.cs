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

        [SerializeField] private UnitResourceManager _units;
        [SerializeField] private ItemResourceManager _items;

        public static UnitResourceManager units => instance._units;
        public static ItemResourceManager items => instance._items;

        private void OnValidate()
        {
            instance = this;
        }
    }
}