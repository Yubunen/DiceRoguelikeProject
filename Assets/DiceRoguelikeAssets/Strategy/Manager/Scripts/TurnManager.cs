using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike.Strategy
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager manager;
        private List<StrategyActerUnit> units;
        private List<UnitProgressUI> progressUIs;

        private Queue<StrategyActerUnit> turnUnit;

        private void Awake()
        {
            if (manager != null)
            {
                Destroy(gameObject);
                return;
            }
            manager = this;
        }

        public void Init()
        {
            units = new List<StrategyActerUnit>();
            turnUnit = new Queue<StrategyActerUnit>();
            progressUIs = new List<UnitProgressUI>();

            for (int i = 0; i < transform.childCount; i++)
            {
                var unit = transform.GetChild(i).GetComponent<StrategyActerUnit>();
                if (unit != null) units.Add(unit);
            }

            if(units.Count == 0) Debug.LogError("unit not found");

            units.Sort((x, y) =>
            {
                float diff = x.Unit.TotalAbility.speed - y.Unit.TotalAbility.speed;
                if (diff > 0) return 1;
                if (diff < 0) return -1;
                if (x.Unit.ID > y.Unit.ID) return 1;
                return -1;
            });
            
            foreach (var unit in units)
            {
                progressUIs.Add(ProgressUIManager.instance.InstantiateUnitProgressUI(unit.Unit));
            }
            TurnRotate();
        }

        public void TurnRotate()
        {
            if (turnUnit.Count > 0) GiveTurn();
            else StartCoroutine(ProgressTurn());
        }

        private IEnumerator ProgressTurn()
        {
            while (turnUnit.Count == 0)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    var result = units[i].ProgressTurn(Time.deltaTime);
                    if (result <= 0) turnUnit.Enqueue(units[i]);
                    progressUIs[i].SetProgress(result);
                }
                yield return null;
            }
            TurnRotate();
        }

        private void GiveTurn()
        {
            var unit = turnUnit.Dequeue();
            ViewPointMove.Inst.MoveTo(unit.transform);
            StartCoroutine(unit.GetTurn());
        }
        
        public void RemoveUnit(StrategyActerUnit unit)
        {
            if (unit == units![0]) ViewPointMove.Inst?.ResetTarget();

            var index = units.FindIndex(0, (a) => a==unit);
            units.RemoveAt(index);
            progressUIs.RemoveAt(index);
        }
    }
}