using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.ScriptableObjects;

namespace GameScripts.Logic.Units.Enemy
{
    public class EnemyHealth : DamageableBattleunit
    {
        public PlayerMoney PlayerMoney;
        public void SetProperties()
        {
            MaxHealth = 100;
            Health = 100;
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