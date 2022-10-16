using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategyAction
    {
        public ActionSkill skill;
        public Route[] routes;
        public StrategyContainer[] targets;

        public StrategyAction(ActionSkill skill, Vector3Int pos)
        {
            this.skill = skill;
            Range range;
            if (skill is MainSkill) range = (skill as MainSkill).range;
            (routes, targets) = TileMapManager.manager.GetRangeTiles(pos, skill is MainSkill ? (skill as MainSkill).range : new Range(Range.RangeType.FourDirection, (skill as SpecialSkill).Cost));
        }

        public void SetRange(Vector3Int pos)
        {
            (routes, targets) = TileMapManager.manager.GetRangeTiles(pos, skill is MainSkill ? (skill as MainSkill).range : new Range(Range.RangeType.FourDirection, (skill as SpecialSkill).Cost));
        }

        public override string ToString()
        {
            return skill.ToString();
        }
    }
}