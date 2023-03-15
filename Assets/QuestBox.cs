using GameScripts.StaticData.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBox : MonoBehaviour
{
    public QuestData questData;
    public Text titleText;
    public Image background;
    public bool doned = false;

    public void Start()
    {
        titleText.text = questData.questName;
    }
}
