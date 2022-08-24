using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class BaseSkill : MonoBehaviour, IHaveInfo
    {
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        [SerializeField] protected int _grade = 0;
        [SerializeField] protected Sprite _sprite;
        protected ActerUnit _caster;

        protected BaseContainer _container => _caster.Container;
        public int grade => _grade;
        public Sprite sprite => _sprite;
        public ActerUnit caster => _caster;
        public uint ID => throw new System.NotImplementedException();
        public string Name => throw new System.NotImplementedException();

        public void Init(ActerUnit caster) 
        { 
            _caster = caster; 
            Init(); 
        }

        protected virtual void Init() { }
    }
}