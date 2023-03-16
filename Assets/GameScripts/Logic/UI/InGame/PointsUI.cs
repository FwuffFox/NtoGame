using GameScripts.Infrastructure.States;
using GameScripts.Logic.Units.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace GameScripts.Logic.UI.InGame
{
    public class PointsUI : MonoBehaviour
    {
        [FormerlySerializedAs("counter")] [SerializeField] private Text _counter;

        private PlayerMoney _playerMoney;
        public void SetPlayer(PlayerMoney playerMoney)
        {
            _playerMoney = playerMoney;
            _playerMoney.OnMoneyChanged += ChangePointsAmount;
        }

        private void ChangePointsAmount(int newAmount)
        {
            _counter.text = $"Монеты: {newAmount}";
        }
        
        private void OnDestroy()
        {
            _playerMoney.OnMoneyChanged -= ChangePointsAmount;
        }
    }
}
