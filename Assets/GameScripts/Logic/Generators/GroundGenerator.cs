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
		private List<Transform> tile;
		private float posX=0.0f;
		private float posZ=0.0f;
		private int tileCountWithZero;

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
			tileCountWithZero = tile.Count - 1;
			for (int w = 0; w < _mapSize; w++) 
			{
				for (int h = 0;h < _mapSize; h++) 
				{
					var obj = Instantiate(tile[Random.Range(0, tileCountWithZero)], new Vector3(posX, 0, posZ),
						Quaternion.Euler(-90, 0, 0));
					obj.parent = _landFolder;
					posX+=tileStep;
				}
				posZ+=tileStep;
				posX=0;
			}
		}
	}
}
