using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategyAction
    {
        public UnitAction action;
        public Route[] routes;
        public StrategyContainer[] targets;

        public StrategyAction(UnitAction action, Vector3Int pos)
        {
            this.action = action;
            (routes, targets) = TileMapManager.manager.GetRangeTiles(pos, action.skill ? action.skill.range : new Range(Range.RangeType.FourDirection, action.cost));
        }

        public void SetRange(Vector3Int pos)
        {
            (routes, targets) = TileMapManager.manager.GetRangeTiles(pos, action.skill ? action.skill.range : new Range(Range.RangeType.FourDirection, action.cost));
        }

        public override string ToString()
        {
            return action.ToString();
        }
    }
}