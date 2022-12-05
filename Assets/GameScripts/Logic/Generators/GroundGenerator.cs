using System.Collections.Generic;
using System.Linq;
using GameScripts.Extensions;
using GameScripts.Logic.Tiles;
using GameScripts.Services.InputService;
using GameScripts.Services.UnitSpawner;
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
		private List<Tile> spawnedTiles = new();
		private float posX=0.0f;
		private float posZ=0.0f;

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
			_trapsCount = data.mapSize;
			_unitsCount = data.mapSize;
		}
		
		public void Generate()
		{
			_landFolder = Instantiate(new GameObject().With(x => x.name = "Land")).transform;
			GenerateMap();
			PlaceTraps();
			_navMeshSurface.BuildNavMesh();
		}
	
		private void GenerateMap() 
		{
			for (int w = 0; w < _mapSize; w++) 
			{
				for (int h = 0;h < _mapSize; h++) 
				{
					var obj = Instantiate(tiles[Random.Range(0, tiles.Count)].transform, new Vector3(posX, 0, posZ),
						Quaternion.Euler(-90, 0, 0));
					var tile = obj.GetComponent<Tile>();
					spawnedTiles.Add(tile);
					obj.parent = _landFolder;
					if (Random.Range(0, 10) == 0) obj.GetComponentsInChildren<Collider>()[0].enabled = true;
					posX+=tileStep;
				}
				posZ+=tileStep;
				posX=0;
			}
		}

		private void PlaceTraps()
		{
			var tilesWithSpawn = spawnedTiles.Where(tile => tile.HaveSpawnPoint).ToList();
			for (int i = 0; i < _trapsCount; i++)
			{
				if (tilesWithSpawn.Count == 0) break;
				var spawnTile = tilesWithSpawn[Random.Range(0, tilesWithSpawn.Count)];
				_unitSpawner.SpawnTrap(spawnTile.SpawnPoint.position);
				tilesWithSpawn.Remove(spawnTile);
			}
		}
		
		private void PlaceUnits()
		{
			var tilesWithSpawn = spawnedTiles.Where(tile => tile.HaveSpawnPoint).ToList();
			for (int i = 0; i < _unitsCount; i++)
			{
				if (tilesWithSpawn.Count == 0) break;
				var spawnTile = tilesWithSpawn[Random.Range(0, tilesWithSpawn.Count)];
				_unitSpawner.SpawnTrap(spawnTile.SpawnPoint.position);
				tilesWithSpawn.Remove(spawnTile);
			}
		}
	}
}
