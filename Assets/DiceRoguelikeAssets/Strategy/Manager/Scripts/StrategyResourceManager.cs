using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    [ExecuteAlways]
    public class StrategyResourceManager : MonoBehaviour
    {
        private static StrategyResourceManager instance = null;
        private ResourceContainer<StrategyContainer> _containers;
        private ResourceContainer<TileObject> _tiles;

        public static ResourceContainer<StrategyContainer> containers => instance._containers;
        public static ResourceContainer<TileObject> tiles => instance._tiles;


        private void OnValidate()
        {
            instance = this;
        }
    }
}