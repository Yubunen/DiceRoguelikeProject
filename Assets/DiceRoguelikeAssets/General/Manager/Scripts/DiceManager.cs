using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ramdom = UnityEngine.Random;

namespace LSemiRoguelike
{
    public abstract class DiceManager : MonoBehaviour
    {
        protected static DiceManager _instance;
        public static DiceManager Instance => _instance;

        protected PlayerUnit owner;
        protected UnitAction weaponAction;
        protected Dice[] dices;
        protected int power;
        protected System.Action<List<UnitAction>> returnAction;

        private void Awake()
        {
            _instance = this;
        }

        public  void Init(PlayerUnit owner, UnitAction weaponSkill, Dice[] dices, System.Action<List<UnitAction>> action)
        {
            this.owner = owner;
            this.weaponAction = weaponSkill;
            this.dices = dices;
            returnAction = action;
            Init();
        }

        protected abstract void Init();

        public abstract void GetActions(int power);
    }
}