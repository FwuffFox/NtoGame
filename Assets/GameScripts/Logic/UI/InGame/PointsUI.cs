using GameScripts.Infrastructure.States;
using GameScripts.Logic.Units.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameScripts.Logic.UI.InGame
{
    public class PointsUI : MonoBehaviour
    {
        [SerializeField] private Text counter;

        private PlayerMoney _playerMoney;
        public void SetPlayer(PlayerMoney playerMoney)
        {
            _playerMoney = playerMoney;
            _playerMoney.OnMoneyChanged += ChangePointsAmount;
        }

        public void ChangePointsAmount(int newAmount)
        {
            counter.text = $"Монеты: {newAmount}";
        }
        
        private void OnDestroy()
        {
            _playerMoney.OnMoneyChanged -= ChangePointsAmount;
        }
    }
}
