using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Shooting
{
    public abstract class ShootingActerUnit : ShootingContainer
    {
        public new ActerUnit Unit { get { return base.Unit as ActerUnit; } }
        protected float moveSpeed => Unit.TotalAbility.speed;
        protected List<UnitAction> _actions;
        private bool moveable = true;

        Magazine magazine;
        protected abstract void MoveUpdate();

        public override void Init()
        {
            base.Init();
            Unit.SetActionCallback(SetActions);
            magazine = new Magazine();
            _actions = new List<UnitAction>();
            Reload();
        }

        protected void SetActions(List<UnitAction> actions)
        {
            _actions = actions;
        }

        protected void Update()
        {
            if (moveable)
                MoveUpdate();
        }

        protected void Shoot(Vector3 dir)
        {
            if (_actions.Count == 0)
                return;

            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                ShootingContainer target;
                if (hit.transform.TryGetComponent(out target))
                {
                    StartCoroutine(_actions[0].skill?.Cast(target));
                }
            }
        }

        protected void Reload()
        {
            Unit.GetSkill();
        }
    }
}