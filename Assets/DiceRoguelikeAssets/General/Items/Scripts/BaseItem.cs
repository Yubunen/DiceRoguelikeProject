using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    [System.Serializable]
    public abstract class BaseItem : IHaveInfo
    {
        //info
        [SerializeField] protected int _id;
        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        [SerializeField] protected Sprite _sprite;
        [SerializeField] protected Ability _ability;

        protected PlayerUnit _owner;

        public Ability ability => _ability;
        public void Init(PlayerUnit owner)
        {
            _owner = owner;
            Init();
        }

        protected abstract void Init();

        public int ID => _id;
        public string Name => _name;
        public string Description => _description;

        public Sprite Icon => _sprite;
        public PlayerUnit owner => _owner;
    }
}