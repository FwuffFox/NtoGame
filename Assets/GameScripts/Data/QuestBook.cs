using System.Collections.Generic;

namespace GameScripts.Data
{
    public class QuestBook
    {
        public string CurrentQuestName { get; set; }
        public List<Quest> Quests { get; set; }
    }
}