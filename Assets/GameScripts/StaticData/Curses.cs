using GameScripts.Logic.Curses;
using GameScripts.Logic.Player;
using GameScripts.StaticData.Enums;

namespace GameScripts.StaticData
{
    public static class Curses
    {
        public static StackableCurse HealthCurse = new(CurseType.Health, 10, 5);
    }
}