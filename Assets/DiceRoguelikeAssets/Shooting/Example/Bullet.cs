using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Shooting
{
    public class Bullet : MonoBehaviour
    {
        protected MainSkill _skill;

        public void SetSkill(MainSkill skill)
        {
            _skill = skill;
        }

        private void OnTriggerEnter(Collider other)
        {
            ShootingContainer target;
            if (other.TryGetComponent(out target))
            {
                _skill.Cast(target);
            }
        }
    }
}