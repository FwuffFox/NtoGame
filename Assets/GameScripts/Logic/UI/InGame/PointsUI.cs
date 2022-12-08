using GameScripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameScripts.Logic.UI.InGame
{
    public class PointsUI : MonoBehaviour
    {
        [SerializeField] private Text counter;

        private GameLoopState _gameLoopState;

        [Inject]
        private void Construct(GameLoopState gameLoopState)
        {
            _gameLoopState = gameLoopState;
            _gameLoopState.OnPointsAmountChanged += ChangePointsAmount;
        }

        private void OnDestroy()
        {
            _gameLoopState.OnPointsAmountChanged -= ChangePointsAmount;
        }

        public void ChangePointsAmount(int newAmount)
        {
            counter.text = $"Points: {newAmount}";
        }
    }
}
