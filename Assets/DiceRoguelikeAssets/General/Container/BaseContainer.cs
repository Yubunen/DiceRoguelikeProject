using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public enum ContainerType { Base, Skill, Player }
    public abstract class BaseContainer : MonoBehaviour
    {
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        
        [SerializeField] protected BaseUnit _unit;
        [SerializeField] protected UnitStatusUI _statusUI;

        public BaseUnit Unit => _unit;

        public Vector3 Pos => transform.position;

        public void SetUnit(BaseUnit unit) { if (_unit == null) _unit = unit; }

        public virtual void Init()
        {
            _unit.Init(this);
            _unit.getEffect.AddListener(SetStatusUI);
            _statusUI.InitUI(_unit.TotalAbility.maxStatus, _unit.TotalStatus);
        }
        
        public virtual void GetEffect(Effect effect)
        {
            _unit.GetEffect(effect);
        }

        protected virtual void SetStatusUI()
        {
            _statusUI.SetUI(_unit.TotalStatus);
        }
    }
}