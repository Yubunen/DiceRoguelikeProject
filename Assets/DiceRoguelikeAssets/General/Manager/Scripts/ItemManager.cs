using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEngine.Windows;
using UnityEditor;
#endif

namespace LSemiRoguelike
{
    public class ItemManager : MonoBehaviour
    {
        protected static ItemManager _instance;
        public static ItemManager Instance => _instance ?? (_instance = new ItemManager());
        [SerializeField] private List<MainSkill> _skills;
        [SerializeField] private int _diceCount;
        [SerializeField] private List<Dice> _dices;

        private PlayerUnit _owner;
        private Weapon _weapon;
        private Parts _armParts, _legParts, _bodyParts;
        private List<Accessory> _accessories = new List<Accessory>();
        private List<Consumable> _consumables = new List<Consumable>();

        public PlayerUnit Owner => _owner;
        public Weapon Weapon => _weapon;
        public Parts ArmParts => _armParts;
        public Parts LegParts => _legParts;
        public Parts BodyParts => _bodyParts;
        public Accessory[] Accessories => _accessories.ToArray();
        public Consumable[] Consumables => _consumables.ToArray();
        public Dice[] Dices => _dices.ToArray();

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Init(PlayerUnit owner)
        {
            this._owner = owner;
            
            _weapon.Init(owner);
            _armParts.Init(owner);
            _bodyParts.Init(owner);
            _legParts.Init(owner);
            _accessories.ForEach((a) => a.Init(owner));
        }

        public Ability GetAbility()
        {
            Ability ability = new Ability();
            ability += _weapon.ability;
            ability += _armParts.ability;
            ability += _legParts.ability;
            ability += _bodyParts.ability;

            _accessories.ForEach((a) => ability += a.ability);
            return ability;
        }

        public UnitAction GetWeaponAction()
        {
            return new UnitAction(_weapon.Cost,_weapon.Skill);
        }

        public void AttackSub()
        {
            _armParts.PowerGenerate();
        }

        public void DamagedSub()
        {
            _bodyParts.PowerGenerate();
        }

        public void SpecialSub()
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

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }
        public void SetArmParts(Parts parts)
        {
            _armParts = parts;
            if (_owner) parts.Init(Owner);
        }
        public void SetBodyParts(Parts parts)
        {
            _bodyParts = parts;
        }
        public void SetLegParts(Parts parts)
        {
            _legParts = parts;
        }

        public void AddAceesory(Accessory accessory)
        {
            _accessories.Add(accessory);
        }
        public void RemoveAceesory(int index)
        {
            _accessories.RemoveAt(index);
        }

        public void AddDice()
        {
            _dices.Add(new Dice());
        }
        public void RemoveDice(int index)
        {
            _dices.RemoveAt(index);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ItemManager))]
    public class ItemManagerEditor : Editor
    {
        int weaponIndex, armPartsIndex, bodyPartsIndex, legPartsIndex;
        List<int> accessoryIndexes;
        string[] weapons,armParts,bodyParts,legParts,accessories;


        bool accessoryGroup = false, diceGroup = false;
        private void OnEnable()
        {
            ItemManager script = (ItemManager)target;

            weapons = GeneralResourceManager.items.Weapons.GetItemNames();
            armParts = GeneralResourceManager.items.ArmParts.GetItemNames();
            bodyParts = GeneralResourceManager.items.BodyParts.GetItemNames();
            legParts = GeneralResourceManager.items.LegParts.GetItemNames();
            accessories = GeneralResourceManager.items.accessories.GetItemNames();

            for (int i = 0; i < weapons.Length; i++) if (weapons[i] == script.Weapon?.Name) weaponIndex = i;
            for (int i = 0; i < armParts.Length; i++) if (armParts[i] == script.ArmParts?.Name) armPartsIndex = i;
            for (int i = 0; i < bodyParts.Length; i++) if (bodyParts[i] == script.BodyParts?.Name) bodyPartsIndex = i;
            for (int i = 0; i < legParts.Length; i++) if (legParts[i] == script.LegParts?.Name) legPartsIndex = i;

            accessoryIndexes = new List<int>();
            for (int i = 0; i < accessories.Length; i++)
            {
                for (int j = 0; j < script.Accessories?.Length; j++)
                {
                    if (script.Accessories[j].Name == accessories[i]) accessoryIndexes.Add(i);
                }
            }
        }
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            serializedObject.Update();
            ItemManager script = (ItemManager)target;

            var select = 0;
            GUIContent weaponLabel = new GUIContent("Weapon");
            select = EditorGUILayout.Popup(weaponLabel, weaponIndex, weapons);
            if (weaponIndex != select)
            {
                script.SetWeapon(new Weapon(GeneralResourceManager.items.Weapons.GetByName(weapons[select])));
                weaponIndex = select;
            }

            GUIContent armPartsLabel = new GUIContent("Arm Parts");
            select = EditorGUILayout.Popup(armPartsLabel, armPartsIndex, armParts);
            if (armPartsIndex != select)
            {
                script.SetArmParts(new Parts(GeneralResourceManager.items.ArmParts.GetByName(armParts[select])));
                armPartsIndex = select;
            }

            GUIContent bodyPartsLabel = new GUIContent("Body Parts");
            select = EditorGUILayout.Popup(bodyPartsLabel, bodyPartsIndex, bodyParts);
            if (bodyPartsIndex != select)
            {
                script.SetBodyParts(new Parts(GeneralResourceManager.items.BodyParts.GetByName(bodyParts[select])));
                bodyPartsIndex = select;
            }

            GUIContent legPartsLabel = new GUIContent("Leg Parts");
            select = EditorGUILayout.Popup(legPartsLabel, legPartsIndex, legParts);
            if (legPartsIndex != select)
            {
                script.SetLegParts(new Parts(GeneralResourceManager.items.LegParts.GetByName(legParts[select])));
                legPartsIndex = select;
            }

            //Accessory
            accessoryGroup = EditorGUILayout.BeginFoldoutHeaderGroup(accessoryGroup, "Accessories");
            if (accessoryGroup)
            {
                EditorGUILayout.BeginVertical("GroupBox");
                for (int i = 0; i < accessoryIndexes.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(i + ": ");
                    GUILayout.Label(accessories[accessoryIndexes[i]]);

                    if (GUILayout.Button("Remove"))
                    {
                        accessoryIndexes.RemoveAt(i);
                        script.RemoveAceesory(i);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.BeginHorizontal();
                select = EditorGUILayout.Popup(legPartsIndex, accessories);
                if (GUILayout.Button("Add"))
                {
                    accessoryIndexes.Add(select);
                    script.AddAceesory(new Accessory(GeneralResourceManager.items.accessories.GetByName(accessories[select])));
                }
                GUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();


            //Dice
            EditorGUILayout.Separator();
            diceGroup = EditorGUILayout.BeginFoldoutHeaderGroup(diceGroup, "Dices");
            if (diceGroup)
            {

                EditorGUILayout.BeginVertical("GroupBox");

                var dicesProp = serializedObject.FindProperty("_dices");
                SerializedProperty diceProp = null;
                for (int i = 0; i < dicesProp.arraySize; i++)
                {
                    diceProp = dicesProp.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(diceProp);

                    if (GUILayout.Button("Remove"))
                    {
                        script.RemoveDice(i);
                    }
                }

                if (GUILayout.Button("Add"))
                {
                    script.AddDice();
                }
                EditorGUILayout.EndVertical();
            }
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
#endif
}