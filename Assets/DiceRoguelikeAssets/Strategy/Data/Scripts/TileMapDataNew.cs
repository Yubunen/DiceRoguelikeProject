using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    [System.Serializable]
    public struct UnitWithPos
    {
        public BaseUnit unit;
        public StrategyContainer container;
        public Vector3Int pos;

        public UnitWithPos(StrategyContainer container)
        {
            unit = GeneralResourceManager.units.GetByID(container.Unit.ID);
            this.container = StrategyResourceManager.GetContainerByType(container.GetType());
            pos = container.cellPos;
        }
    }

    public struct TileWithPos
    {
        public TileObject tile;
        public Vector3Int pos;

        public TileWithPos(TileObject tile)
        {
            this.tile = StrategyResourceManager.tiles.GetByID(tile.ID);
            pos = tile.cellPos;
        }
    }

    [CreateAssetMenu(fileName = "TileMapData", menuName = "DiceRogueLike/Strategy/TileMapData", order = 11)]
    public class TileMapDataNew : ScriptableObject
    {
        public List<ObjWithPos> tiles;
        public List<UnitWithPos> units;
    }
}