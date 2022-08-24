using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "UnitResourceContainer", menuName = "DiceRogueLike/Resource/UnitResourceContainer", order = 1)]
    public class UnitResourceContainer : ScriptableObject
    {
        [SerializeField] private ResourceContainer<BaseUnit> _baseUnits;
        [SerializeField] private ResourceContainer<SkillUnit> _skillUnits;
        [SerializeField] private ResourceContainer<PlayerUnit> _playerUnits;

        public ResourceContainer<BaseUnit> baseUnits => _baseUnits;
        public ResourceContainer<SkillUnit> skillUnits => _skillUnits;
        public ResourceContainer<PlayerUnit> playerUnits => _playerUnits;


        public BaseUnit GetByID(uint id)
        {
            return null;
        }
    }
}