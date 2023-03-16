using GameScripts.Logic.Units.Player;

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
            QuestManager.Instance.HaveTorch = true;
            QuestManager.Instance.Kills++;
            OnlineManager.OnlineManager.Manager.Kills++;
            base.OnHealthReachZero();
        }
    }
}