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
        protected MainSkill weaponSkill;
        protected List<Dice> dices;
        protected int power;
        protected System.Action<List<MainSkill>> returnAction;

        private void Awake()
        {
            _instance = this;
        }

        public  void Init(PlayerUnit owner, MainSkill weaponSkill, List<Dice> dices, System.Action<List<MainSkill>> action)
        {
            this.owner = owner;
            this.weaponSkill = weaponSkill;
            this.dices = dices;
            returnAction = action;
        }

        protected abstract void Init();

        public abstract void GetActions(int power);
    }
}