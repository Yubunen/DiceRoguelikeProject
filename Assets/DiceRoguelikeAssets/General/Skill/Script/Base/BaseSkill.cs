using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseSkill : ScriptableObject
    {
        [SerializeField]private SkillInfo _Info;
        private ActerUnit _caster;

        public SkillInfo Info => _Info;
        protected BaseContainer Container => _caster.Container;
        public ActerUnit Caster => _caster;

        public void Init(ActerUnit caster) 
        {
            _caster = caster; 
            Init(); 
        }

        protected virtual void Init() { }
    }
}
