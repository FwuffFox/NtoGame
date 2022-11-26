namespace GameScripts.Logic.Debuffs
{
    public class PeriodicalTimedDebuff
    {
        public PeriodicalTimedDebuffType DebuffType;
        public int Duration;
        public float DebuffValuePerSecond;

        public PeriodicalTimedDebuff(PeriodicalTimedDebuffType debuffType, int duration, float debuffValuePerSecond)
        {
            DebuffType = debuffType;
            Duration = duration;
            DebuffValuePerSecond = debuffValuePerSecond;
        }
    }

    public enum PeriodicalTimedDebuffType
    {
        Health, 
        Stamina
    }
}