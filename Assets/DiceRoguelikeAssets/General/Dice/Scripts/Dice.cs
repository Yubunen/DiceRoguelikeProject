using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public class Dice
    {
        [SerializeField] private int _maxCost;
        [SerializeField] private ActionSkill up, down, left, right, front, back;
        private ActionSkill[] _skills;
        public ActionSkill[] Skills => _skills;
        public int MaxCost => _maxCost;
        public int NowCost() 
        {
            int cost = 0;
            foreach(var skill in _skills) cost += skill.Cost;
            return cost; 
        }

        public Dice(Dice other)
        {
            _maxCost = other._maxCost;
            up = other.up;
            down = other.down;
            left = other.left;
            right = other.right;
            front = other.front;
            back = other.back;
        }

        public void Init()
        {
            _skills = new ActionSkill[6] {
                GameObject.Instantiate(right),
                GameObject.Instantiate(up),
                GameObject.Instantiate(front),
                GameObject.Instantiate(left),
                GameObject.Instantiate(down),
                GameObject.Instantiate(back)
            };

            for (int i = 0; i < 6; i++)
                _skills[i].Init(PlayerManager.Instance.Player);
        }

        public ActionSkill ChangeSkill(int index, ActionSkill newSkill)
        {
            var existingSkill = _skills[index];
            _skills[index] = newSkill;
            return existingSkill;
        }

        public override string ToString()
        {
            return $"{_skills}";
        }
    }
}