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
		[SerializeField]
		private float tileStep=3.16f;
		[SerializeField]
		private List<Tile> tiles;
		private readonly List<Tile> _spawnedTiles = new();
		private List<Tile> _tilesWithSpawn;
		private float posX=0.0f;
		private float posZ=0.0f;
		private int tileNumber;
		
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
		}
		
		public void GenerateMapAndTraps()
		{
			_landFolder = Instantiate(new GameObject().With(x => x.name = "Land")).transform;
			GenerateMap();
			_tilesWithSpawn = _spawnedTiles.Where(tile => tile.HaveSpawnPoint).ToList();
			PlaceTraps();
			_navMeshSurface.BuildNavMesh();
		}
	
		private void GenerateMap() 
		{
			for (int w = 0; w < _mapSize; w++) 
			{
				for (int h = 0;h < _mapSize; h++) 
				{
					if (w==0&&h==0||w==_mapSize-1&&h==_mapSize-1) tileNumber=0; //1 тайл-всегда обычный
					else tileNumber=Random.Range(0,tiles.Count);
					var obj = Instantiate(tiles[tileNumber].transform, new Vector3(posX, 0, posZ),
						Quaternion.Euler(-90, 0, 0));
					var tile = obj.GetComponent<Tile>();
					_spawnedTiles.Add(tile);
					obj.parent = _landFolder;
					if (tile.HaveCursedObject)
					{
						if (Random.Range(0, 10) == 0)
						{
							var curseObj = obj.GetComponentInChildren<TestCurseObject>();
							curseObj.Enable(true);
							curseObj.CurseType = GetRandomCurseType();
						}
					}
					posX+=tileStep;
				}
				posZ+=tileStep;
				posX=0;
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

		private readonly CurseType[] _curseTypes = { CurseType.Health, CurseType.Stamina };
		private CurseType GetRandomCurseType()
		{
			var random = Random.Range(0, 101);
			return _curseTypes[random % _curseTypes.Length];
		}
	}
}
