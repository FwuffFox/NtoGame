using GameScripts.StaticData.Enums;

namespace GameScripts.Logic.Debuffs
{
    public class SimpleDebuff
    {
        public SimpleDebuffType DebuffType;
        public int Duration;
        public float DebuffValue;

        public SimpleDebuff(SimpleDebuffType debuffType, int duration, float debuffValue)
        {
            DebuffType = debuffType;
            Duration = duration;
            DebuffValue = debuffValue;
        }
    }
}