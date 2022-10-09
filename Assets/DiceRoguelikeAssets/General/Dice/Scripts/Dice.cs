using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class Dice
    {
        [SerializeField] private int _maxCost;
        [SerializeField] private UnitAction up, down, left, right, front, back;
        private UnitAction[] _parts;

        public int MaxCost => _maxCost;
        public int NowCost() 
        {
            int cost = 0;
            foreach(var part in _parts) cost += part.cost;
            return cost; 
        }

        public UnitAction GetParts(int num) => _parts[num];

        public void Init(PlayerUnit caster)
        {
            var obj = new GameObject("Dice").transform;
            obj.parent = caster.transform;
            _parts = new UnitAction[6] {
                new UnitAction(right.cost, GameObject.Instantiate(right.skill)),
                new UnitAction(up.cost, GameObject.Instantiate(up.skill)),
                new UnitAction(front.cost, GameObject.Instantiate(front.skill)),
                new UnitAction(left.cost, GameObject.Instantiate(left.skill)),
                new UnitAction(down.cost, GameObject.Instantiate(down.skill)),
                new UnitAction(back.cost, GameObject.Instantiate(back.skill))
            };

            for (int i = 0; i < 6; i++)
                _parts[i].skill?.Init(caster);
        }

        public MainSkill ChangeSkill(int index, MainSkill newSkill)
        {
            var existingSkill = _parts[index].skill;
            _parts[index].SetSkill(newSkill);
            return existingSkill;
        }

        public bool ChangeCost(int index, int newCost)
        {
            _parts[index].SetCost(newCost);
            return MaxCost > NowCost();
        }

        public override string ToString()
        {
            return $"{_parts}";
        }
    }
}