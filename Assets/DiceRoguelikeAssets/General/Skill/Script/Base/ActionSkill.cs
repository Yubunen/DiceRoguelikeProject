using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class ActionSkill : BaseSkill
    {
        [Space]
        [SerializeField]private int _cost;
        public int Cost => _cost;
    }
}