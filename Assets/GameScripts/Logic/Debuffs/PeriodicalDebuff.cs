using GameScripts.StaticData.Enums;

namespace GameScripts.Logic.Debuffs
{
    public class PeriodicalDebuff
    {
        public PeriodicalDebuffType DebuffType;
        public int Duration;
        public float DebuffValuePerSecond;

        public PeriodicalDebuff(PeriodicalDebuffType debuffType, int duration, float debuffValuePerSecond)
        {
            DebuffType = debuffType;
            Duration = duration;
            DebuffValuePerSecond = debuffValuePerSecond;
        }
    }
}