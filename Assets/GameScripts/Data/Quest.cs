using System;
using System.Collections.Generic;

namespace GameScripts.Data
{
    public class Quest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaskDescription { get; set; }

        public bool Done { get; set; } = false;
    }
}