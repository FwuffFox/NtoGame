namespace GameScripts.Logic.Debuffs
{
    public class TimedDebuff
    {
        public TimedDebuffType DebuffType;
        public int Duration;
        public float DebuffValue;

        public TimedDebuff(TimedDebuffType debuffType, int duration, float debuffValue)
        {
            DebuffType = debuffType;
            Duration = duration;
            DebuffValue = debuffValue;
        }
    }

    public enum TimedDebuffType
    {
        MaxHealth,
        MaxStamina,
        Speed
    }
}