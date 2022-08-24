using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LSemiRoguelike
{
    [System.Serializable]
    public class Conditions
    {
        private BaseUnit _unit;
        private List<BaseCondition> _conditions = new List<BaseCondition>();

        public void Init(BaseUnit unit)
        {
            _unit = unit;
        }

        public void Add(BaseCondition condition)
        {
            int i = 0;
            bool isExist = false;
            foreach (var con in _conditions)
            {
                condition.Interact(con);
                if (con.priority < condition.priority) i++;
                if (con.GetType() == condition.GetType()) isExist = true;
            }
            if (!isExist)
            {
                _conditions.Insert(i, condition);
                condition.Add(_unit);
            }
        }

        public void Activate()
        {
            for(int i = 0; i < _conditions.Count; i++)
            {
                if (!_conditions[i].isActivate) 
                {
                    _conditions[i].Remove(_unit);
                    _conditions.Remove(_conditions[i]);
                    i--;
                }
                else _conditions[i].Activate(_unit, _conditions);
            }
        }
    }
}