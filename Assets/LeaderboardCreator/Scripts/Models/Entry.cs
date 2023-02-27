namespace Dan.Models
{
    [System.Serializable]
    public struct Entry
    {
        public string Username { get; set; }
        public int Score { get; set; }
        public ulong Date { get; set; }
        public string Extra { get; set; }

        [field: System.NonSerialized] internal string NewUsername { get; set; }
    }
}