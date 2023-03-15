using GameScripts.Logic.Campfire;
using GameScripts.Logic.Traps;
using GameScripts.Logic.Units.Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrefabLevelInfo : MonoBehaviour
{
    public List<Campfire> campfires = new();
    public List<EnemyAI> enemies = new();
    public List<BearTrap> traps = new();

    [ContextMenu("Save")]
    void Save()
    {
        //враги
        enemies.Clear();
        Transform folder = transform.Find("Enemies");
        for (int i = 0; i < folder.childCount; i++)
        {
            folder.GetChild(i).GetComponent<NavMeshAgent>().enabled = false;
            enemies.Add(folder.GetChild(i).GetComponent<EnemyAI>());
        }

        //ловушки
        traps.Clear();
        folder = transform.Find("Traps");
        for (int i = 0; i < folder.childCount; i++)
            traps.Add(folder.GetChild(i).GetComponentInChildren<BearTrap>());

        //костры
        campfires.Clear();
        folder = transform;
        for (int i = 0; i < folder.childCount; i++)
            if (folder.GetChild(i).GetComponentInChildren<Campfire>())
                campfires.Add(folder.GetChild(i).GetComponentInChildren<Campfire>());

        Debug.Log("ѕроверь сохранение уровн€");
    }
}

