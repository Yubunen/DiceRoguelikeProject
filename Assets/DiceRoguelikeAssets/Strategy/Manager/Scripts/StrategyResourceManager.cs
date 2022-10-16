using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    [CreateAssetMenu(fileName = "StrategyResourceManager", menuName = "Dice Roguelike/Strategy/StrategyResourceManager", order = 1)]
    public class StrategyResourceManager : ScriptableObject
    {
        private static StrategyResourceManager instance = null;
        [SerializeField] private List<TileObject> _tileObjects;
        [SerializeField] private StrategyContainer _baseUnitPrefab;
        [SerializeField] private StrategySkillUnit _skillUnitPrefab;
        [SerializeField] private StrategyPlayerUnit _playerUnitPrefab;


        private void OnValidate()
        {
            instance = this;
        }
        public static TileObject GetTileById(int id)
        {
            return instance._tileObjects.Find((t)=>t.ID==id);
        }

        public static StrategyContainer GetContainerByType(UnitInfo.UnitType type)
        {
            if (!instance) return null;
            switch (type)
            {
                case UnitInfo.UnitType.ObjectUnit:
                    return Instantiate(instance._baseUnitPrefab);
                case UnitInfo.UnitType.SkillUnit:
                    return Instantiate(instance._skillUnitPrefab);
                case UnitInfo.UnitType.PlayerUnit:
                    return Instantiate(instance._playerUnitPrefab);
            }
            return null;
        }
    }
}