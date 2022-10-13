using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Strategy
{
    public class TileObject : MonoBehaviour
    {
        [SerializeField] private int _id;
        private Vector3Int _cellPos;

        public Vector3Int CellPos => _cellPos;

        public int ID => _id;

        public void Init(Vector3Int pos)
        {
            _cellPos = pos;
        }
        public void Init()
        {
            _cellPos = TileMapManager.manager.WorldToCell(transform.position);
        }
    }
}