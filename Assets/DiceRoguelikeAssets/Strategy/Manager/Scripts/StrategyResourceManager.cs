using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    [ExecuteAlways]
    public class StrategyResourceManager : MonoBehaviour
    {
        private static StrategyResourceManager instance = null;

        [SerializeField] private StrategyContainerResourceContainer _container;
        [SerializeField] private TileObjectResourceContainer _tile;

        public static StrategyContainerResourceContainer container => instance._container;
        public static TileObjectResourceContainer tile => instance._tile;

        private void OnValidate()
        {
            instance = this;
        }
    }
}