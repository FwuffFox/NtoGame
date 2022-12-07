using System.Collections.Generic;
using System.Linq;
using GameScripts.Extensions;
using GameScripts.Logic.Tiles;
using GameScripts.Logic.Enemy;
using GameScripts.Services.UnitSpawner;
using GameScripts.StaticData.Enums;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace GameScripts.Logic.Generators
{
	public class GroundGenerator : MonoBehaviour
	{
		private int _mapSize;
		private LevelData.LevelCurses[] _curses;
		[SerializeField]
		private float tileStep=3.16f;
		[SerializeField]
		private List<Tile> tiles;
		private readonly List<Tile> _spawnedTiles = new();
		private readonly List<CurseObject> _curseObjects = new();
		private List<Tile> _tilesWithSpawn;
		private float _posX;
		private float _posZ;
		private int _tileNumber;
		
		[SerializeField] private NavMeshSurface _navMeshSurface;
		private int _trapsCount;
		private int _unitsCount;

		private Transform _landFolder;

		private IUnitSpawner _unitSpawner;

		[Inject]
		public void Construct(IUnitSpawner unitSpawner)
		{
			_unitSpawner = unitSpawner;
		}

		public void SetProperties(LevelData data)
		{
			_mapSize = data.mapSize;
			_trapsCount = data.trapsCount;
			_unitsCount = data.unitCount;
			_curses = data.Curses;
		}
		
		public void GenerateMapAndTraps()
		{
			_landFolder = Instantiate(new GameObject().With(x => x.name = "Land")).transform;
			GenerateMap();
			_tilesWithSpawn = _spawnedTiles.Where(tile => tile.HaveSpawnPoint).ToList();
			PlaceTraps();
			PlaceCursedObjects();
			_navMeshSurface.BuildNavMesh();
		}

		private void GenerateMap() 
		{
			for (int w = 0; w < _mapSize; w++) 
			{
				for (int h = 0;h < _mapSize; h++) 
				{
					if (w==0&&h==0||w==_mapSize-1&&h==_mapSize-1) _tileNumber=0; //1 тайл-всегда обычный
					else _tileNumber=Random.Range(0,tiles.Count);
					var obj = Instantiate(tiles[_tileNumber].transform, new Vector3(_posX, 0, _posZ),
						Quaternion.Euler(-90, 0, 0));
					var tile = obj.GetComponent<Tile>();
					_spawnedTiles.Add(tile);
					obj.parent = _landFolder;
					if (tile.HaveCursedObject)
					{
						_curseObjects.Add(obj.GetComponentInChildren<CurseObject>());
					}
					_posX+=tileStep;
				}
				_posZ+=tileStep;
				_posX=0;
			}
		}

		private void PlaceTraps()
		{
			for (int i = 0; i < _trapsCount; i++)
			{
				if (_tilesWithSpawn.Count == 0) break;
				var spawnTile = _tilesWithSpawn[Random.Range(0, _tilesWithSpawn.Count)];
				_unitSpawner.SpawnTrap(spawnTile.SpawnPoint.position);
				_tilesWithSpawn.Remove(spawnTile);
			}
		}
		
		public void PlaceUnits(GameObject player)
		{
			for (int i = 0; i < _unitsCount; i++)
			{
				if (_tilesWithSpawn.Count == 0) break;
				var spawnTile = _tilesWithSpawn[Random.Range(0, _tilesWithSpawn.Count)];
				var enemy = _unitSpawner.SpawnEnemy(spawnTile.SpawnPoint.position, EnemyType.Warrior);
				enemy.GetComponent<EnemyAI>().SetPlayer(player);
				_tilesWithSpawn.Remove(spawnTile);
			}
		}
		
		private void PlaceCursedObjects()
		{
			foreach (var levelCurse in _curses)
			{
				for (int i = 0; i < levelCurse.Amount; i++)
				{
					if (_curseObjects.Count == 0)
					{
						Debug.LogError("Not enough curse objects to place all curses");
						return;
					}
					var cursedObj = _curseObjects[Random.Range(0, _curseObjects.Count)];
					cursedObj.Enable(true);
					cursedObj.CurseType = levelCurse.Type;
					_curseObjects.Remove(cursedObj);
				}
			}
		}
	}
}
