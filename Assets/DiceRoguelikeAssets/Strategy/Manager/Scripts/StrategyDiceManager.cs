using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class StrategyDiceManager : DiceManager
    {
        [SerializeField] private float height = 1, width = 5, throwTime = 1;
        [SerializeField] protected DiceObject dicePrefab;
        protected List<DiceObject> diceObjects;

        private void Awake()
        {
            _instance = this;
        }

        protected override void Init()
        {
            dices.ForEach((d) => d.Init(owner));
            diceObjects = new List<DiceObject>();
        }

        public override void GetActions(int power)
        {
            this.power = power;
            DiceSelectUI.inst.SetDiceUI(dices, weaponSkill, power, GetSelected);
        }

        public void GetSelected(bool[] diceUse, bool weaponUse)
        {
            if (diceUse == null && !weaponUse)
            {
                returnAction(new List<MainSkill>());
                return;
            }

            var useDice = new List<Dice>();
            for (int i = 0; i < diceUse.Length; i++)
            {
                if (diceUse[i]) useDice.Add(dices[i]);
            }
            StartCoroutine(RollDice(useDice, weaponUse));
        }

        IEnumerator RollDice(List<Dice> useDice, bool weaponUse)
        {
            var count = useDice.Count;
            var interval = width / (count + 1);
            var results = new List<MainSkill>();

            for (int i = diceObjects.Count; i < count; i++)
            {
                diceObjects.Add(Instantiate(dicePrefab, transform));
                diceObjects[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < count; i++)
            {
                diceObjects[i].gameObject.SetActive(true);
                diceObjects[i].transform.localPosition =
                    new Vector3(-width / 2 + interval * (i + 1), height, 0);
                StartCoroutine(diceObjects[i].RollDice(useDice[i], (p) => results.Add(p)));
                yield return new WaitForSeconds(throwTime / count);
            }

            yield return new WaitUntil(() => results.Count == useDice.Count);
            if (weaponUse) results.Add(weaponSkill);
            returnAction(results);

            foreach (var obj in diceObjects) obj.gameObject.SetActive(false);
        }
    }
}