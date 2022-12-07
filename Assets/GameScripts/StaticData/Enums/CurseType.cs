namespace GameScripts.StaticData.Enums
{
    public enum CurseType
    {
        Health,
        Stamina, 
        Damage
    }

    public static partial class Extensions
    {
        public static string CurseTypeToString(this CurseType type) => type switch
        {
            CurseType.Health => "Проклятие здоровья",
            CurseType.Stamina => "Проклятие усталости",
            CurseType.Damage => "Проклятие слабости",
            _ => "Ошибка"
        };
    }
}