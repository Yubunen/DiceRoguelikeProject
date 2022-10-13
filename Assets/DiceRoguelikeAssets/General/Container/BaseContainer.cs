using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [ExecuteAlways]
    public enum ContainerType { Base, Skill, Player }
    public abstract class BaseContainer : MonoBehaviour
    {        
        [SerializeField] protected BaseUnit _unit;
        [SerializeField] protected UnitStatusUI _statusUI;
        [SerializeField] protected SpriteRenderer _renderer;
        public BaseUnit Unit => _unit;

        public Vector3 Pos => transform.position;

        public void SetUnit(BaseUnit unit) { if (_unit == null) _unit = unit; }

        public virtual void Init()
        {
            _unit.Init(this);
            _statusUI.InitUI(_unit.TotalAbility.maxStatus, _unit.TotalStatus);
        }
        
        public virtual void GetEffect(Effect effect)
        {
            _unit.GetEffect(effect);
        }

        public void UpdateStatusUI()
        {
            _statusUI.SetUI(_unit.TotalStatus);
        }
        private void OnValidate()
        {
            if (_unit) _renderer.sprite = _unit.Info.Sprite;
        }
    }
}