using GameScripts.Logic.Units.Player;

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
            QuestManager.Instance.HaveTorch = true;
            QuestManager.Instance.Kills++;
            OnlineManager.OnlineManager.Manager.Kills++;
            base.OnHealthReachZero();
        }
    }
}