using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "PlayerUnit", menuName = "Dice Roguelike/Unit/PlayerUnit", order = 0)]
    public class PlayerUnit : ActerUnit
    {
        protected DiceManager diceManager => DiceManager.Instance;
        protected PlayerManager playerManager => PlayerManager.Instance;

        protected Status buffStatus;
        //public DiceManager diceManager { get { return _diceManager; } }

        protected override void Init()
        {
            base.Init();
        }

        protected override void SetAbility()
        {
            _totalAbility = _ability;
            _totalAbility += playerManager.GetAbility();
            foreach (Buff buff in _buffs) _totalAbility += buff.ability;
        }

        public override void Attack()
        {
            playerManager.ArmParts.PowerGenerate();
        }

        public override void Special()
        {
            playerManager.SpecialSub();
        }

        protected override void Damaged()
        {
            playerManager.DamagedSub();
        }
        public override void Passive()
        {
            playerManager.Passive();
        }
        public override void SetActionCallback(System.Action<List<ActionSkill>> action)
        {
            diceManager.Init(this, playerManager.Weapon.Skill, playerManager.Dices, action);
        }

        public override void GetSkill()
        {
            diceManager.GetActions((int)TotalStatus.power);
        }
    }
}