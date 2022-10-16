using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEngine.Windows;
using UnityEditor;
#endif

namespace LSemiRoguelike
{
    public class PlayerManager : MonoBehaviour
    {
        protected static PlayerManager _instance;
        public static PlayerManager Instance => _instance ?? (_instance = new PlayerManager());

        [SerializeField] private PlayerData _playerData;

        private PlayerUnit _player;
        private Weapon _weapon;
        private Parts _armParts, _legParts, _bodyParts;
        private List<Accessory> _accessories = new List<Accessory>();
        private List<Consumable> _consumables = new List<Consumable>();
        private List<ActionSkill> _skills = new List<ActionSkill>();
        private List<Dice> _dices = new List<Dice>();

        public PlayerUnit Player => _player;
        public Weapon Weapon => _weapon;
        public Parts ArmParts => _armParts;
        public Parts LegParts => _legParts;
        public Parts BodyParts => _bodyParts;
        public Accessory[] Accessories => _accessories.ToArray();
        public Consumable[] Consumables => _consumables.ToArray();
        public Dice[] Dices => _dices.ToArray();
        private bool _skillInit = false;

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            InitFromData();
            InitSkills();
        }

        private void InitFromData()
        {
            _player = Instantiate(_playerData.playerPrefab);
            _weapon = Instantiate(_playerData.weaponPrefab);
            _armParts = Instantiate(_playerData.armPartsPrefab);
            _bodyParts = Instantiate(_playerData.bodyPartsPrefab);
            _legParts = Instantiate(_playerData.legPartsPrefab);

            foreach (var acc in _playerData.accessoriePrefabs) _accessories.Add(Instantiate(acc));
            foreach (var con in _playerData.consumablePrefabs) _consumables.Add(Instantiate(con));
            foreach (var dice in _playerData.dices) _dices.Add(new Dice(dice));

            _skills = _playerData.skills;
        }

        public void InitSkills()
        {
            _weapon.Init();
            _armParts.Init();
            _bodyParts.Init();
            _legParts.Init();

            foreach (var acc in _accessories) acc.Init();
            foreach (var dice in _dices) dice.Init();

            _skillInit = true;
        }

        public Ability GetAbility()
        {
            Ability ability = new Ability();
            //ability += _weapon.ability;
            //ability += _armParts.ability;
            //ability += _legParts.ability;
            //ability += _bodyParts.ability;

            //_accessories.ForEach((a) => ability += a.ability);
            return ability;
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
            if(_skillInit)
                _accessories[_accessories.Count - 1].Init();
        }
        public void RemoveAceesory(int index)
        {
            _accessories.RemoveAt(index);
        }

        public void AddDice(Dice dice)
        {
            _dices.Add(dice);
        }

        public void RemoveDice(int index)
        {
            _dices.RemoveAt(index);
        }
    }
    /*
#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerManager))]
    public class ItemManagerEditor : Editor
    {
        int weaponIndex, armPartsIndex, bodyPartsIndex, legPartsIndex;
        List<int> accessoryIndexes;
        string[] weapons,armParts,bodyParts,legParts,accessories;


        bool accessoryGroup = false;
        private void OnEnable()
        {
            PlayerManager script = (PlayerManager)target;

            weapons = GeneralResourceManager.items.Weapons.GetItemNames();
            armParts = GeneralResourceManager.items.ArmParts.GetItemNames();
            bodyParts = GeneralResourceManager.items.BodyParts.GetItemNames();
            legParts = GeneralResourceManager.items.LegParts.GetItemNames();
            accessories = GeneralResourceManager.items.Accessories.GetItemNames();

            if (!script.Weapon && weapons.Length > 0) script.SetWeapon(Instantiate(GeneralResourceManager.items.Weapons.GetByName(weapons[0])));
            if (!script.ArmParts && armParts.Length > 0) script.SetArmParts(Instantiate(GeneralResourceManager.items.ArmParts.GetByName(armParts[0])));
            if (!script.BodyParts && bodyParts.Length > 0) script.SetBodyParts(Instantiate(GeneralResourceManager.items.BodyParts.GetByName(bodyParts[0])));
            if (!script.LegParts && legParts.Length > 0) script.SetLegParts(Instantiate(GeneralResourceManager.items.LegParts.GetByName(legParts[0])));

            for (int i = 0; i < weapons.Length; i++) if (weapons[i] == script.Weapon?.Info.Name) weaponIndex = i;
            for (int i = 0; i < armParts.Length; i++) if (armParts[i] == script.ArmParts?.Info.Name) armPartsIndex = i;
            for (int i = 0; i < bodyParts.Length; i++) if (bodyParts[i] == script.BodyParts?.Info.Name) bodyPartsIndex = i;
            for (int i = 0; i < legParts.Length; i++) if (legParts[i] == script.LegParts?.Info.Name) legPartsIndex = i;

            accessoryIndexes = new List<int>();
            for (int i = 0; i < accessories.Length; i++)
            {
                for (int j = 0; j < script.Accessories?.Length; j++)
                {
                    if (script.Accessories[j].Info.Name == accessories[i]) accessoryIndexes.Add(i);
                }
            }
        }
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            serializedObject.Update();
            PlayerManager script = (PlayerManager)target;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_playerPrefab"));

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
                    script.AddAceesory(new Accessory(GeneralResourceManager.items.Accessories.GetByName(accessories[select])));
                }
                GUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();


            //Dice
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_dices"));

            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
#endif
    */
}