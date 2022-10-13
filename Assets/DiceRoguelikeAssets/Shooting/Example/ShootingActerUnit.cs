using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Shooting
{
    public abstract class ShootingActerUnit : ShootingContainer
    {
        public new ActerUnit Unit { get { return base.Unit as ActerUnit; } }
        protected float moveSpeed => Unit.TotalAbility.speed;
        protected List<MainSkill> _actions;
        private bool moveable = true;

        Magazine magazine;
        protected abstract void MoveUpdate();

        public override void Init()
        {
            base.Init();
            Unit.SetActionCallback(SetActions);
            magazine = new Magazine();
            _actions = new List<MainSkill>();
            Reload();
        }

        protected void SetActions(List<ActionSkill> actions)
        {
            foreach (var skill in actions)
            {
                if (skill is MainSkill)
                {
                    _actions.Add(Instantiate(skill as MainSkill));
                    _actions[_actions.Count - 1].Init(Unit);
                }
            }
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
                    _actions[0]?.Cast(target);
                }
            }
        }

        protected void Reload()
        {
            Unit.GetSkill();
        }
    }
}