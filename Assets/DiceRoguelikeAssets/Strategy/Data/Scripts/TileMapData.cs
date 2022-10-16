using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    [System.Serializable]
    public struct UnitWithPos
    {
        public BaseUnit unit;
        public Vector3Int pos;

        public UnitWithPos(StrategyContainer container)
        {
            unit = container.Unit;
            pos = container.cellPos;
        }
    }

    [System.Serializable]
    public struct TileWithPos
    {
        public TileObject tile;
        public Vector3Int pos;

        public TileWithPos(TileObject tile)
        {
            this.tile = StrategyResourceManager.GetTileById(tile.ID);
            pos = tile.CellPos;
        }
    }

    [CreateAssetMenu(fileName = "TileMapData", menuName = "Dice Roguelike/Strategy/TileMapData", order = 1)]
    public class TileMapData : ScriptableObject
    {
        public List<TileWithPos> tiles;
        public List<UnitWithPos> units;
        public Vector3Int playerPos;
    }
}