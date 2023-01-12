using GameScripts.Logic.Curses;
using GameScripts.StaticData.Enums;

namespace GameScripts.StaticData
{
    public static class Curses
    {
        public static StackableCurse HealthCurse = new(CurseType.Health, 10, 5);
        public static StackableCurse StaminaCurse = new(CurseType.Stamina, 10, 5);
        public static StackableCurse DamageCurse = new(CurseType.Damage, 10, 2);
    }
}