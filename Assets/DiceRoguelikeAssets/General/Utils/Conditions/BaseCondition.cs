using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public abstract class BaseCondition
    {
        public int _duration;
        protected int _priority;
        public bool isActivate => _duration > 0;
        public int priority => _priority;


        public abstract void Activate(BaseUnit unit, List<BaseCondition> others);
        public abstract void Interact(BaseCondition other);
        public abstract void Add(BaseUnit unit);
        public abstract void Remove(BaseUnit unit);

        public override string ToString()
        {
            return GetType().ToString() + "," + _duration + "," + priority;
        }
    }
}