using System.Collections.Generic;
using GameScripts.Extensions;
using GameScripts.Services.InputService;
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
		private List<Transform> tiles;
		private float posX=0.0f;
		private float posZ=0.0f;

		[SerializeField] private NavMeshSurface _navMeshSurface;

		private Transform _landFolder;

		public void SetProperties(LevelData data)
		{
			_mapSize = data.mapSize;
		}
		
		public void Generate()
		{
			_landFolder = Instantiate(new GameObject().With(x => x.name = "Land")).transform;
			GenerateMap();
			_navMeshSurface.BuildNavMesh();
		}
	
		private void GenerateMap() 
		{
			for (int w = 0; w < _mapSize; w++) 
			{
				for (int h = 0;h < _mapSize; h++) 
				{
					var obj = Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(posX, 0, posZ),
						Quaternion.Euler(-90, 0, 0));
					obj.parent = _landFolder;
					if (Random.Range(0, 10) == 0) obj.GetComponentsInChildren<Collider>()[0].enabled = true;
					posX+=tileStep;
				}
				posZ+=tileStep;
				posX=0;
			}
		}
	}
}
