using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    [ExecuteAlways]
    public class StrategyResourceManager : MonoBehaviour
    {
        private static StrategyResourceManager instance = null;
        [SerializeField] private ResourceContainer<TileObject> _tiles;
        [SerializeField] private StrategyContainer _baseUnitPrefab;
        [SerializeField] private StrategySkillUnit _skillUnitPrefab;
        [SerializeField] private StrategyPlayerUnit _playerUnitPrefab;

        public static ResourceContainer<TileObject> tiles => instance._tiles;


        private void OnValidate()
        {
            instance = this;
            _tiles.Sort();
        }

        public static StrategyContainer GetContainerByType(System.Type type)
        {
            Debug.Log(type);
            if (!instance) return null;
            if (type == typeof(BaseUnit))
                return Instantiate(instance._baseUnitPrefab);
            else if (type == typeof(SkillUnit))
                return Instantiate(instance._skillUnitPrefab);
            else if (type == typeof(PlayerUnit))
                return Instantiate(instance._playerUnitPrefab);
            Debug.Log("not");
            return null;
        }
    }
}