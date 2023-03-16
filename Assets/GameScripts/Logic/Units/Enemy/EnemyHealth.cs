using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.ScriptableObjects;

namespace GameScripts.Logic.Units.Enemy
{
    public class EnemyHealth : DamageableBattleunit
    {
        public PlayerMoney PlayerMoney;
        public int multiplyHp=1;
        public void SetProperties()
        {
            MaxHealth = 100* multiplyHp;
            Health = 100* multiplyHp;
        }

        protected override void OnHealthReachZero()
        {
            PlayerMoney.Money += 100;
            QuestManager.questManager.haveTorch = true;
            QuestManager.questManager.kills++;
            OnlineManager.onlineManager.kills++;
            base.OnHealthReachZero();
        }
    }
}