using System;
using System.Collections.Generic;
using System.Linq;
using GameScripts.Extensions;
using GameScripts.Logic.Campfire;
using GameScripts.Logic.Navigation;
using GameScripts.Logic.Tiles;
using GameScripts.Logic.Units.Enemy;
using GameScripts.Services.Factories;
using GameScripts.Services.UnitSpawner;
using GameScripts.StaticData.Enums;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace GameScripts.Logic.Generators
{
	public class GroundGenerator : MonoBehaviour
	{
		private int _mapSize;
		private LevelData.LevelCurses[] _curses;
		private const float TileStep = 3.16f;
		private List<Tile> _tiles = new();
		private readonly List<Tile> _spawnedTiles = new();
		private readonly List<CurseObject.CurseObject> _curseObjects = new();
		private List<Tile> _tilesWithSpawn;
		private float _posX;
		private float _posZ;
		private LevelData.XZCoord[] _coords;
		public LevelSO levelSO;
		public PrefabLevelInfo prefabLevel;

		[SerializeField] private NavMeshSurface _navMeshSurface;
		private int _trapsCount;
		private int _unitsCount;

		private Transform _landFolder;
		private PrefabLevelInfo _spawnedLevel;
		public EnemyData enemyData;

		private IUnitSpawner _unitSpawner;
        private Transform obj;

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
			foreach (var tileWithPower in data.GeneratorTiles)
			{
				for (int i = 0; i < tileWithPower.Power; i++)
					_tiles.Add(tileWithPower.Tile);
			}
			_coords = data.CheckpointsCoors;
		}
		
		public void GenerateMapAndTraps()
		{
			if (prefabLevel)
			{
				_spawnedLevel=Instantiate(prefabLevel);
				//костры
				for (int i=0;i<_spawnedLevel.campfires.Count;i++)
                {
					if (_spawnedLevel.campfires[i].Type == CampfireType.Start)
						_unitSpawner.SpawnCampfire(_spawnedLevel.campfires[i].transform.position, CampfireType.Start);
					if (_spawnedLevel.campfires[i].Type == CampfireType.Checkpoint)
						_unitSpawner.SpawnCampfire(_spawnedLevel.campfires[i].transform.position, CampfireType.Checkpoint);
					if (_spawnedLevel.campfires[i].Type == CampfireType.Final)
						_unitSpawner.SpawnCampfire(_spawnedLevel.campfires[i].transform.position, CampfireType.Final);
					Destroy(_spawnedLevel.campfires[i].gameObject);
				}
				//генерация из префаба
				_navMeshSurface.BuildNavMesh();
			}
			else
            {
				//старая генерация
				_landFolder = Instantiate(new GameObject().With(x => x.name = "Land")).transform;
				GenerateMap();
				_tilesWithSpawn = _spawnedTiles.Where(tile => tile.HaveSpawnPoint).ToList();
				PlaceTraps();
				PlaceCursedObjects();
				_navMeshSurface.BuildNavMesh();
			}
		}

		private void GenerateMap() 
		{
			int i = 0;
			for (int w = 0; w < _mapSize; w++) 
			{
				for (int h = 0;h < _mapSize; h++)
				{
					if (!levelSO)
					{
						//автогенерация
						int tileNumber;
						if (w == 0 && h == 0 || w == _mapSize - 1 && h == _mapSize - 1 || _coords.Contains(new LevelData.XZCoord { X = w, Z = h }))
							tileNumber = 0;
						else tileNumber = Random.Range(0, _tiles.Count);

						obj = Instantiate(_tiles[tileNumber].transform, new Vector3(_posX, 0, _posZ),
							Quaternion.Euler(-90, 0, 0));
					}
					else   //грузим уровень
						obj = Instantiate(levelSO.objs[i].Tile.transform, new Vector3(_posX, 0, _posZ),
							Quaternion.Euler(-90, 0, 0));
					var tile = obj.GetComponent<Tile>();
					if (tile.HaveSpawnPoint)
					{	//можем заспавнить объект
						if (w == 0 && h == 0)
						{
							_unitSpawner.SpawnCampfire(tile.SpawnPoint.position, CampfireType.Start);
							tile.HaveSpawnPoint = false;
						}
						else if (w == _mapSize - 1 && h == _mapSize - 1)
						{
							_unitSpawner.SpawnCampfire(tile.SpawnPoint.position, CampfireType.Final);
							tile.HaveSpawnPoint = false;
						}
						else if (_coords.Contains(new LevelData.XZCoord { X = w, Z = h }))
						{
							_unitSpawner.SpawnCampfire(tile.SpawnPoint.position, CampfireType.Checkpoint);
							tile.HaveSpawnPoint = false;
						}
					}
					_spawnedTiles.Add(tile);
					obj.parent = _landFolder;
					if (tile.HaveCursedObject)
					{	//проклятие
						_curseObjects.Add(obj.GetComponentInChildren<CurseObject.CurseObject>());
					}
					_posX+=TileStep;
					i++;
				}
				_posZ+=TileStep;
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
				spawnTile.HaveSpawnPoint = false;
				_tilesWithSpawn.Remove(spawnTile);
			}
		}
		
		public void PlaceUnits(GameObject player)
		{
			if (prefabLevel)
			{
				for (int i = 0; i < _spawnedLevel.enemies.Count; i++)
				{
					_spawnedLevel.enemies[i].GetComponent<NavMeshAgent>().enabled = true;
					GameObject enemy = _spawnedLevel.enemies[i].gameObject;
					var health = enemy.GetComponent<EnemyHealth>()
						.With(x =>
						{
							x.SetProperties();
						});

					var ai = enemy.GetComponent<EnemyAI>()
						.With(x =>
						{
							x.SetProperties(enemyData);
							x.EnemyHealth = enemy.GetComponent<EnemyHealth>();
						});

					var mover = enemy.GetComponent<EnemyMover>()
						.With(x => x.SetProperties(enemyData));

					var attacker = enemy.GetComponent<EnemyAttacker>()
						.With(x => x.SetProperties(enemyData));

					enemy.GetComponent<EnemyAnimator>()
						.With(animator =>
						{
							mover.OnSpeedChange += animator.SetSpeed;
							attacker.OnAttack += animator.SetAttack;
							health.OnBattleUnitDeath += animator.SetDeath;
						});

					enemy.GetComponent<EnemyUI>()
						.With(ui =>
						{
							ui.SetTarget(health);
						});
					_spawnedLevel.enemies[i].SetPlayer(player);
				}
				//генерация из префаба
				_navMeshSurface.BuildNavMesh();
			}
			else
			{	//старая генерация
				for (int i = 0; i < _unitsCount; i++)
				{
					if (_tilesWithSpawn.Count == 0) break;
					var spawnTile = _tilesWithSpawn[Random.Range(0, _tilesWithSpawn.Count)];
					var enemy = _unitSpawner.SpawnEnemy(spawnTile.SpawnPoint.position, EnemyType.Warrior);
					enemy.GetComponent<EnemyAI>().SetPlayer(player);
					_tilesWithSpawn.Remove(spawnTile);
				}
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
