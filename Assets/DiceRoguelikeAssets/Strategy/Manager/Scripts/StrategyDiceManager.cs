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
            diceObjects = new List<DiceObject>();
        }

        public override void GetActions(int power)
        {
            this.power = power;
            DiceSelectUI.Inst.SetDiceUI(dices, weaponAction, power, GetSelected);
        }

        public void GetSelected(bool[] diceUse, bool weaponUse)
        {
            if (diceUse == null && !weaponUse)
            {
                returnAction(new List<ActionSkill>());
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
            var results = new int[count];

            for (int i = diceObjects.Count; i < count; i++)
            {
                diceObjects.Add(Instantiate(dicePrefab, transform));
                diceObjects[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < count; i++)
            {
                int j = i;
                diceObjects[i].gameObject.SetActive(true);
                diceObjects[i].transform.localPosition = new Vector3(-width / 2 + interval * (i + 1), height, 0);
                diceObjects[i].RollDice(useDice[i], (p) => { results[j] = p; count--; });
                yield return new WaitForSeconds(throwTime / count);
            }
            yield return new WaitUntil(() => count == 0);
            
            var skills = new List<ActionSkill>();
            if (weaponUse) skills.Add(weaponAction);
            for (int i = 0; i < results.Length; i++) skills.Add(useDice[i].Skills[results[i]]);
            foreach (var obj in diceObjects) obj.gameObject.SetActive(false);

            returnAction(skills);
        }
    }
}