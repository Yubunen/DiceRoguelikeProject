using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class MainSkill : ActionSkill
    {
        [SerializeField] private Range _range;
        [Header("Target layer")]
        [SerializeField] private bool emptyTarget;
        [SerializeField] private bool selfTarget;
        [SerializeField] private bool friendlyTarget;
        [SerializeField] private bool enemyTarget;
        [SerializeField] private float _delayTime;

        public Range range => _range;
        public bool[] targetLayer => new bool[] {emptyTarget, selfTarget, friendlyTarget, enemyTarget};
        public float delayTime => _delayTime;
        public abstract void Cast(BaseContainer target);

        public bool TargetCheck(BaseContainer target)
        {
            return (target == null && emptyTarget)
                || (target == Container && selfTarget)
                ||( target.tag == Container.tag && friendlyTarget)
                || (target.tag != Container.tag && enemyTarget);
        }
    }
}