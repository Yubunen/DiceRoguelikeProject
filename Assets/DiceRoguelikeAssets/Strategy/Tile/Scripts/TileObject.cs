using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class TileObject : MonoBehaviour, IHaveInfo
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [HideInInspector] public Vector3Int cellPos;

        public int ID => _id;
        public string Name => _name;
        public string Description => _description;

        public Sprite Icon => _icon;

        public void Init(Vector3Int pos)
        {
            cellPos = pos;
        }
        public void Init()
        {
            cellPos = TileMapManager.manager.WorldToCell(transform.position);
        }
    }
}