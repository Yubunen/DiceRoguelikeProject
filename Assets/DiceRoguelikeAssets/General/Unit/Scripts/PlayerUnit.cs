using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class PlayerUnit : ActerUnit
    {
        protected DiceManager diceManager => DiceManager.Instance;
        protected ItemManager itemManager => ItemManager.Instance;

        protected Status buffStatus;
        //public DiceManager diceManager { get { return _diceManager; } }

        protected override void Init()
        {
            diceManager.transform.localPosition = new Vector3(0, 2, 0);
            itemManager.Init(this);

            base.Init();
        }

        protected override void SetAbility()
        {
            _totalAbility = _initAbility;
            _totalAbility += itemManager.GetAbility();
            foreach (Buff buff in _buffs) _totalAbility += buff.ability;
        }

        public override void Attack()
        {
            itemManager.AttackSub();
        }

        public override void Special()
        {
            itemManager.SpecialSub();
        }

        protected override void Damaged()
        {
            itemManager.DamagedSub();
        }
        public override void Passive()
        {
            itemManager.Passive();
        }
        public override void SetActionCallback(System.Action<List<UnitAction>> action)
        {
            diceManager.Init(this, itemManager.GetWeaponAction(), itemManager.Dices, action);
        }

        public override void GetSkill()
        {
            diceManager.GetActions((int)TotalStatus.power);
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/Dice Rogue Like/Player Unit", false, 10)]
        static void CreateDiceUnit(MenuCommand menuCommand) { CreateUnit(menuCommand, typeof(PlayerUnit)); }
#endif
    }
}