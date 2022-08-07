using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


namespace LSemiRoguelike.Strategy
{
    [CreateAssetMenu(fileName ="StrategyDatas", menuName ="DiceRogueLike/Strategy/DatasManager", order = 10)]
    public class StrategyDataManager : ScriptableObject
    {
        private static StrategyDataManager instance;
        [SerializeField] private List<TileMapData> _tileMapDatas = new List<TileMapData>();
        public static string path => Path.GetDirectoryName(AssetDatabase.GetAssetPath(instance));

        public static List<TileMapData> tileMapDatas => instance._tileMapDatas;
        private void OnValidate()
        {
            instance = this;
        }
        public static void AddMapData(TileMapData data)
        {
            instance._tileMapDatas.Add(data);
        }
    }
}