using System.Collections.Generic;

namespace LSemiRoguelike
{
    [System.Serializable]
    public abstract class BaseCondition
    {
        public bool isActivate => _duration > 0;
        protected int _duration;

        public abstract void Interact(BaseCondition other);
        public abstract void Activate(BaseUnit unit);
    }
}