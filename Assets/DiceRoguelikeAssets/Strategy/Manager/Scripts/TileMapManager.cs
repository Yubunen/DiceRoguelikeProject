using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEngine.Windows;
using UnityEditor;
#endif

namespace LSemiRoguelike.Strategy
{
	public class TileMapManager : MonoBehaviour
	{
		public static TileMapManager manager;

		[SerializeField] Tilemap groundTileMap, unitTileMap, trapTileMap;

		TileObject[][] groundTileArray;
		StrategyContainer[][] unitTileArray;
		//GameObject[][] trapTileArray;

		int x_anchor, y_anchor, x_size, y_size;

		public TileMapData tileMapData;

		private void Awake()
		{
			manager = this;
		}

		private void Start()
		{
			SetTilemap();
			TurnManager.manager.Init();
		}

        public void TileMapSave()
        {
			tileMapData.tiles = new List<ObjWithPos>();
			tileMapData.units = new List<ObjWithPos>();


			for (int i = 0; i < groundTileMap.transform.childCount; i++)
			{
				Transform child = groundTileMap.transform.GetChild(i);
				tileMapData.tiles.Add(new ObjWithPos(child.GetComponent<TileObject>().ID, WorldToCell(child.position)));
			}
			for (int i = 0; i < unitTileMap.transform.childCount; i++)
			{
				Transform child = unitTileMap.transform.GetChild(i);
				tileMapData.units.Add(new ObjWithPos(child.GetComponent<BaseContainer>().Unit.ID, WorldToCell(child.position)));
			}
			UnityEditor.EditorUtility.SetDirty(tileMapData);
		}

        public void TileMapLoad()
        {
			if (tileMapData == null)
			{
				Debug.LogError("Tile map data is empty");
				return;
			}

			foreach (Transform parent in new Transform[] { groundTileMap.transform, unitTileMap.transform, trapTileMap.transform })
            {
				for (int i = parent.childCount; i > 0; i--)
				{
					DestroyImmediate(parent.GetChild(i - 1).gameObject);
				}
			}

			if (tileMapData.tiles != null)
            {
                foreach (var tile in tileMapData.tiles)
                {
                    var obj = StrategyResourceManager.tiles.GetByID(tile.objID);
                    if (obj == null)
                    {
                        continue;
                    }
                    var pos = CellToWorld(tile.pos) + obj.transform.position;
                    Instantiate(obj, pos, Quaternion.identity, groundTileMap.transform).Init(tile.pos);
                }
            }

            if (tileMapData.units != null)
            {
                foreach (var unit in tileMapData.units)
                {
                    var pos = CellToWorld(unit.pos);
                    var container = Instantiate(StrategyResourceManager.containers.GetByID(unit.objID / 100), pos, Quaternion.identity, unitTileMap.transform);
                    //container.SetUnit(Instantiate(GeneralResourceManager.unit.GetByID(unit.objID), container.transform));
                }
            }
        }

        public void SetTilemap()
		{
			int tileCount = groundTileMap.transform.childCount;
			int xMax = 0, xMin = 0, yMax = 0, yMin = 0;
			for (int i = 0; i < tileCount; i++)
			{
				Vector3Int tra = groundTileMap.WorldToCell(groundTileMap.transform.GetChild(i).position);

				xMax = xMax > tra.x ? xMax : tra.x;
				yMax = yMax > tra.y ? yMax : tra.y;

				xMin = xMin < tra.x ? xMin : tra.x;
				yMin = yMin < tra.y ? yMin : tra.y;
			}

			x_anchor = -xMin;
			y_anchor = -yMin;

			x_size = xMax - xMin + 1;
			y_size = yMax - yMin + 1;

			groundTileArray = new TileObject[x_size][];
			unitTileArray = new StrategyContainer[x_size][];

			for (int i = 0; i < x_size; i++)
			{
				groundTileArray[i] = new TileObject[y_size];
				unitTileArray[i] = new StrategyContainer[y_size];
			}

			for (int i = 0; i < tileCount; i++)
			{
				Transform child = groundTileMap.transform.GetChild(i);
				Vector3Int pos = WorldToCell(child.position);
				groundTileArray[pos.x + x_anchor][pos.y + y_anchor] = child.gameObject.GetComponent<TileObject>();
				//test
				child.GetComponent<TileObject>().Init();
			}
			for (int i = 0; i < unitTileMap.transform.childCount; i++)
			{
				Transform child = unitTileMap.transform.GetChild(i);
				Vector3Int pos = WorldToCell(child.position);
				unitTileArray[pos.x + x_anchor][pos.y + y_anchor] = child.gameObject.GetComponent<StrategyContainer>();
				//test
				
				child.GetComponent<StrategyContainer>().Init();
			}
			//for (int i = 0; i < trapTileMap.transform.childCount; i++)
			//{
			//    Transform child = trapTileMap.transform.GetChild(i);
			//    Vector3Int pos = WorldToCell(child.position);
			//    trapTileArray[pos.x + x_anchor][pos.y + y_anchor] = child.gameObject;
			//}
			//TilemapDataUpdate();
		}

		public Vector3Int WorldToCell(Vector3 pos)
		{
			return unitTileMap.WorldToCell(pos);
		}
		public Vector3 CellToWorld(Vector3Int pos)
		{
			return unitTileMap.CellToWorld(pos) + new Vector3(0.5f, 0f, 0.5f);
		}
		public bool IsPosAvail(Vector3Int pos)
		{
			var result = (pos.x + x_anchor < x_size && pos.y + y_anchor < y_size &&
				pos.x + x_anchor >= 0 && pos.y + y_anchor >= 0);
			return result;
		}

		public TileObject GetGroundTile(Vector3Int pos)
		{
			if (!IsPosAvail(pos)) return null;

			return groundTileArray[pos.x + x_anchor][pos.y + y_anchor];
		}

		public StrategyContainer GetUnitTile(Vector3Int pos)
		{
			if (!IsPosAvail(pos)) return null;

			return unitTileArray[pos.x + x_anchor][pos.y + y_anchor];
		}

		public void SetUnitTile(Vector3Int pos, StrategyContainer unit)
		{
			if (!IsPosAvail(pos))
				return;
			unitTileArray[pos.x + x_anchor][pos.y + y_anchor] = unit;
		}

		//public GameObject GetTrapTile(Vector3Int pos)
		//{
		//    if (!IsPosAvail(pos))return null;

		//    return trapTileArray[pos.x + x_anchor][pos.y + y_anchor];
		//}
		//public void SetTrapTile(Vector3Int pos, GameObject trap)
		//{
		//    if (!IsPosAvail(pos))
		//        return;

		//    trapTileArray[pos.x + x_anchor][pos.y + y_anchor] = trap;
		//}

		public bool MoveUnit(Vector3Int origin, Vector3Int target)
		{
			if (!IsPosAvail(origin) || !IsPosAvail(target)
				|| unitTileArray[target.x + x_anchor][target.y + y_anchor] != null)
				return false;

			unitTileArray[target.x + x_anchor][target.y + y_anchor] = unitTileArray[origin.x + x_anchor][origin.y + y_anchor];
			unitTileArray[origin.x + x_anchor][origin.y + y_anchor] = null;
			return true;
		}

		public void RemoveUnit(StrategyContainer unit)
		{
			var pos = unit.cellPos;
			if (!IsPosAvail(pos))
				return;
			unitTileArray[pos.x + x_anchor][pos.y + y_anchor] = null;
		}

		public bool CheckBlocked(Vector3Int pos)
		{
			if (!IsPosAvail(pos)) return false;

			return unitTileArray[pos.x + x_anchor][pos.y + y_anchor] == null && groundTileArray[pos.x + x_anchor][pos.y + y_anchor] != null;
		}

		public (Route[], StrategyContainer[]) GetRangeTiles(Vector3Int pos, Range range)
		{
			List<Route> tiles = new List<Route>();
			List<StrategyContainer> targetUnits = new List<StrategyContainer>();
			List<int> unitLocate = new List<int>();

			Route root = new Route(pos, null, 0);

			tiles.Add(root);
			targetUnits.Add(GetUnitTile(pos));
			
			List<Vector3Int> nonSight = new List<Vector3Int>();

			if (range.rangeType == Range.RangeType.Linear)
            {
				List<Vector3Int> dirList = new List<Vector3Int> {
					new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0),
					new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0),
					new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0),
					new Vector3Int(-1, 1, 0), new Vector3Int(-1, -1, 0)
				};

				foreach (var dir in dirList)
				{
					Route preRoute = root;
					for (int i = 1; i <= range.maxRange; i++)
					{
						Vector3Int next = pos + dir * i;
						if (IsPosAvail(next))
						{
							tiles.Add(new Route(next, preRoute, i));
							var obj = GetUnitTile(next);
							if (obj)
							{
								if (i >= range.minRange)
                                {
									unitLocate.Insert(0, tiles.Count - 1);
									targetUnits.Add(obj);
                                }
								if (range.ignoreBlocked)
									preRoute = new Route(next, preRoute, i);
								else break;
							}
						}
						else break;
					}
				}
            }
			else
            {
				List<Vector3Int> moveArr = new List<Vector3Int>
				{
					new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0),
					new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0)
				};
				if (range.rangeType == Range.RangeType.Square)
				{
					moveArr.Add(new Vector3Int(1, 1, 0));
					moveArr.Add(new Vector3Int(1, -1, 0));
					moveArr.Add(new Vector3Int(-1, 1, 0));
					moveArr.Add(new Vector3Int(-1, -1, 0));
				}

				foreach (var move in moveArr)
				{
					if (CheckBlocked(root.pos + move))
						tiles.Add(new Route(root.pos + move, root, root.dist + 1));
				}

				int counter = 0;
				while (tiles.Count > counter)
				{
					Route cur = tiles[counter];
					counter++;

					var obj = GetUnitTile(cur.pos);
					if (obj)
					{
						unitLocate.Insert(0, counter-1);
						targetUnits.Add(obj);

						if (!range.ignoreBlocked)
							continue;
					}

					if (cur.dist >= range.maxRange)
						continue;

					foreach (var move in moveArr)
					{
						var nextPos = cur.pos + move;
						if (cur.preRoute.pos == nextPos || !IsPosAvail(nextPos))
							continue;

						var temp = tiles.Find(a => { return a.pos == nextPos; });

						if (temp == null)
							tiles.Add(new Route(nextPos, cur, cur.dist + 1));
					}
				}
			}
			foreach(var loc in unitLocate)
            {
				tiles.RemoveAt(loc);
            }

			//Min Range
            for (int i = 0; i < tiles.Count; i++)
			{
				if (tiles[i].dist < range.minRange)
				{
					tiles.RemoveAt(i);
					i--;
				}
			}

			return (tiles.ToArray(), targetUnits.ToArray());
		}
    }

#if UNITY_EDITOR
	[CustomEditor(typeof(TileMapManager))]
	public class TileMapEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var tileMapManager = target as TileMapManager;
			if (tileMapManager.tileMapData)
			{
				if (GUILayout.Button("Save"))
				{
					tileMapManager.TileMapSave();
				}

				if (GUILayout.Button("Load"))
				{
					tileMapManager.TileMapLoad();
				}
				if (GUILayout.Button("New"))
				{
					tileMapManager.tileMapData = CreateTileMapData();
				}
			}
			else
			{
				if (GUILayout.Button("Create"))
				{
					tileMapManager.tileMapData = CreateTileMapData();
				}
			}
		}

		private TileMapData CreateTileMapData()
		{
			TileMapData tileMapData = ScriptableObject.CreateInstance<TileMapData>();
			var relatePath = StrategyDataManager.path + "\\" + nameof(TileMapData);
			var absolutePath = System.IO.Path.GetDirectoryName(Application.dataPath) + "\\" + relatePath;


			if (File.Exists(absolutePath + ".asset"))
			{
				int i = 1;
				while (File.Exists(absolutePath + i + ".asset")) ;
				relatePath += i;
			}

			AssetDatabase.CreateAsset(tileMapData, relatePath + ".asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();
			//Selection.activeObject = tileMapData;
			StrategyDataManager.AddMapData(tileMapData);
			Debug.Log("Create map data as " + relatePath + ".asset");
			return tileMapData;
		}
	}

#endif
}