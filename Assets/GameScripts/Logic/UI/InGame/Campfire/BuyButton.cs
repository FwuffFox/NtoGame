using UnityEngine;

namespace GameScripts.Logic.UI.InGame
{
    public enum BuyButtonType {
        Health,
        Damage,
        Speed,
        Heal
    }

    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private BuyButtonType _type;

        [SerializeField] private int _price;
        public GameObject Player;

        // Called from Unity
        public void OnClick()
        {
            if (Player.GetComponent<PlayerMoney>().Money < _price) return;

            switch (_type) 
            {
                case BuyButtonType.Health:
                    Player.GetComponent<PlayerHealth>.MaxHealth += 10;
                    Player.GetComponent<PlayerHealth>.Health += 10;
                    break;

                case BuyButtonType.Damage:
                    Player.GetComponent<PlayerAttack>.Damage += 5;
                    break;

                case BuyButtonType.Speed:
                    Player.GetComponent<Movement>.Speed + 1;
                    break;
            }
        }
    }
}