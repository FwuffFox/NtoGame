using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    void OnEnable()
    {
        var jsonTextFile = Resources.Load<TextAsset>("Text/jsonFile01");
        Debug.Log(jsonTextFile);
    }
}
