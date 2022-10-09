using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseSkill : MonoBehaviour, IHaveInfo
    {
        [SerializeField] protected int _id;
        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        [SerializeField] protected int _grade = 0;
        [SerializeField] protected Sprite _sprite;
        protected ActerUnit _caster;

        protected BaseContainer _container => _caster.Container;
        public int grade => _grade;
        public ActerUnit caster => _caster;

        public int ID => _id;
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _sprite;

        public void Init(ActerUnit caster) 
        { 
            _caster = caster; 
            Init(); 
        }

        protected virtual void Init() { }
    }
}