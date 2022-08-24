using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike
{
    public class ItemManager : MonoBehaviour
    {
        protected static ItemManager _instance;
        public static ItemManager Instance => _instance;
        [SerializeField] uint _weaponID, _armPartsID, _legPartsID, _bodyPartsID;
        [SerializeField] List<uint> _accessoryID;
        [SerializeField] List<Dice> _dices;

        protected PlayerUnit _owner;
        protected Weapon _weapon;
        protected Parts _armParts, _legParts, _bodyParts;
        protected List<Accessory> _accessories;

        public List<Dice> Dices => _dices;

        private void Awake()
        {
            _instance = this;
        }

        public void Init(PlayerUnit owner)
        {
            this._owner = owner;
            
            _weapon = new Weapon(GeneralResourceManager.items.weapons.GetByID(_weaponID));
            _weapon.Init(owner);
            
            _armParts = new Parts(GeneralResourceManager.items.parts.GetByID(_armPartsID));
            _armParts.Init(owner);
            
            _bodyParts = new Parts(GeneralResourceManager.items.parts.GetByID(_bodyPartsID));
            _bodyParts.Init(owner);

            _legParts = new Parts(GeneralResourceManager.items.parts.GetByID(_legPartsID));
            _legParts.Init(owner);

            _accessories = new List<Accessory>();
            _accessoryID.ForEach((id) => {
                var acc = new Accessory(GeneralResourceManager.items.accessories.GetByID(id));
                acc.Init(owner);
                _accessories.Add(acc);
            });
        }

        public Ability GetAbility()
        {
            Ability ability = new Ability();
            return ability;
        }

        public MainSkill GetWeaponSkill()
        {
            return _weapon.skill;
        }

        public void AttackSub()
        {
            _armParts.PowerGenerate();
        }
        public void DamagedSub()
        {
            _bodyParts.PowerGenerate();
        }
        public void MoveSub()
        {
            _legParts.PowerGenerate();
        }

        public void Passive()
        {
            foreach (var acc in _accessories)
            {
                acc.Activate();
            }
        }
    }
}