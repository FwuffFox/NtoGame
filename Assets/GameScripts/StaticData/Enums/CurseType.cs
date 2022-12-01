namespace GameScripts.StaticData.Enums
{
    public enum CurseType
    {
        Health,
        Stamina
    }

    public static partial class Extensions
    {
        public static string CurseTypeToString(this CurseType type) => type switch
        {
            CurseType.Health => "Проклятие здоровья",
            CurseType.Stamina => "Проклятие выносливости",
            _ => "Ошибка"
        };
    }
}