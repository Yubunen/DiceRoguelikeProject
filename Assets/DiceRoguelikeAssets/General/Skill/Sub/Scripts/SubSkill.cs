using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public abstract class SubSkill : BaseSkill
    {
        [SerializeField] private float _cost;
        private float _nowPower;

        protected override void Init()
        {
            _nowPower = 0;
        }

        public void SupplyPower(float power)
        {
            _nowPower += power;
            if (_nowPower > _cost)
            {
                StartCoroutine(Cast());
                _nowPower -= _cost;
            }
        }

        protected abstract IEnumerator Cast();
    }
}