using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LSemiRoguelike
{
    [CreateAssetMenu(fileName = "SkillUnit", menuName = "Dice Roguelike/Unit/SkillUnit", order = 0)]
    public class SkillUnit : ActerUnit
    {
        [SerializeField] List<ActionSkill> actionSkillPrefabs;
        [SerializeField] List<SubSkill> attSubPrefabs, dmgSubPrefabs, specialSubPrefabs;
        [SerializeField] List<PassiveSkill> passivePrefabs;

        [SerializeField] float attPowerGen = 1f, movePowerGen = 1f, dmgPowerGen = 1f;

        List<ActionSkill> _actions;
        List<SubSkill> _attSub, _specialSub, _dmgSub;
        List<PassiveSkill> _passive;

        Action<List<ActionSkill>> actionCallBacck;

        protected override void Init()
        {
            _actions = new List<ActionSkill>();
            _attSub = new List<SubSkill>();
            _specialSub = new List<SubSkill>();
            _dmgSub = new List<SubSkill>();
            _passive = new List<PassiveSkill>();

            actionSkillPrefabs.ForEach((s) => _actions.Add(Instantiate(s)));
            attSubPrefabs.ForEach((s) => _attSub.Add(Instantiate(s)));
            specialSubPrefabs.ForEach((s) => _specialSub.Add(Instantiate(s)));
            dmgSubPrefabs.ForEach((s) => _dmgSub.Add(Instantiate(s)));
            passivePrefabs.ForEach((s) => _passive.Add(Instantiate(s)));

            _actions.ForEach((s) => s.Init(this));
            _attSub.ForEach((s) => s.Init(this));
            _specialSub.ForEach((s) => s.Init(this));
            _dmgSub.ForEach((s) => s.Init(this));
            _passive.ForEach((s) => s.Init(this));

            base.Init();
        }

        public override void Attack()
        {
            _attSub.ForEach((s) => s.SupplyPower(attPowerGen));
        }
        public override void Special()
        {
            _specialSub.ForEach((s) => s.SupplyPower(movePowerGen));
        }

        protected override void Damaged()
        {
            _dmgSub.ForEach((s) => s.SupplyPower(dmgPowerGen));
        }

        public override void Passive()
        {
            _passive.ForEach((s) => s.Passive());
        }

        public override void GetSkill()
        {
            actionCallBacck(_actions);
        }

        public override void SetActionCallback(Action<List<ActionSkill>> action)
        {
            actionCallBacck = action;
        }
    }
}