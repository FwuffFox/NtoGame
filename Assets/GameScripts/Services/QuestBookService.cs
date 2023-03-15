using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameScripts.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace GameScripts.Services
{
    public class QuestBookService
    {
        private const string FileName = "/quests.json";
        [CanBeNull] private static QuestBook _questBook;
        
        private readonly string _path = Application.persistentDataPath + FileName;

        public QuestBookService()
        {
            if (_questBook != null) return;
            if (File.Exists(_path))
            {
                Debug.Log($"Reading QuestBook file at {_path}");
                try
                {
                    _questBook = JsonUtility.FromJson<QuestBook>(_path);
                } 
                catch (ArgumentException e)
                {
                    Debug.Log($"Can't parse file at {_path}");
                }
                return;
            }
            Debug.Log($"Creating QuestBook file at {_path}");
            File.Create(_path);
        }

        public void FinishQuest(Guid id)
        {
            var quest = _questBook!.Quests.First(q => q.Id == id);
            quest.Done = true;
        }

        public void WriteToBookFile(QuestBook questBook)
        {
            _questBook = questBook;
            using var stream = new StreamWriter(_path);
            stream.Write(JsonUtility.ToJson(_questBook, prettyPrint: true));
        }

        public IEnumerable<GameScripts.Data.Quest> GetDoneQuests() => _questBook.Quests.Where(q => q.Done);
        public IEnumerable<GameScripts.Data.Quest> GetUnfinishedQuests() => _questBook.Quests.Where(q => !q.Done);
    }
}