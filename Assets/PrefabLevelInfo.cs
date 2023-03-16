using GameScripts.Logic.Campfire;
using GameScripts.Logic.Traps;
using GameScripts.Logic.Units.Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PrefabLevelInfo : MonoBehaviour
{
    [FormerlySerializedAs("campfires")] public List<Campfire> Campfires = new();
    [FormerlySerializedAs("enemies")] public List<EnemyAI> Enemies = new();
    [FormerlySerializedAs("traps")] public List<BearTrap> Traps = new();

    [ContextMenu("Save")]
    void Save()
    {
        //враги
        Enemies.Clear();
        Transform folder = transform.Find("Enemies");
        for (int i = 0; i < folder.childCount; i++)
        {
            folder.GetChild(i).GetComponent<NavMeshAgent>().enabled = false;
            Enemies.Add(folder.GetChild(i).GetComponent<EnemyAI>());
        }

        //ловушки
        Traps.Clear();
        folder = transform.Find("Traps");
        for (int i = 0; i < folder.childCount; i++)
            Traps.Add(folder.GetChild(i).GetComponentInChildren<BearTrap>());

        //костры
        Campfires.Clear();
        folder = transform;
        for (int i = 0; i < folder.childCount; i++)
            if (folder.GetChild(i).GetComponentInChildren<Campfire>())
                Campfires.Add(folder.GetChild(i).GetComponentInChildren<Campfire>());

        Debug.Log("ѕроверь сохранение уровн€");
    }
}

