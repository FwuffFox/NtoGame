using System;
using System.Linq;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.Enums;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InGame
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private ShopItemType _type;
        
        private int _price;

        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                _priceText.text = $"{_price}";
            }
        }

        [SerializeField] private Text _labelText;

        [SerializeField] private Text _priceText;

        [SerializeReadOnly] public GameObject Player;

        [SerializeField, SerializeReadOnly] private ShopItemInfo _shopItemInfo;

        private void OnEnable()
        {
            _shopItemInfo ??= Resources.LoadAll<ShopItemInfo>("StaticData/ShopInfo")
                .First(x => x.Type == _type);
            Price = _shopItemInfo.Price;
            SetTexts();
        }

        private void OnValidate()
        {
            SetTexts();
            _shopItemInfo ??= Resources.LoadAll<ShopItemInfo>("StaticData/ShopInfo")
                .First(x => x.Type == _type);
            Price = _shopItemInfo.Price;
        }

        private void SetTexts()
        {
            _labelText.text = _type switch
            {
                ShopItemType.Health => "Здоровье",
                ShopItemType.Damage => "Урон",
                ShopItemType.Speed => "Скорость",
                ShopItemType.Heal => "Полное Здоровье",
                _ => throw new ArgumentOutOfRangeException()
            };
            _priceText.text = $"{_price}";
        }

        // Called from Unity
        public void OnClick()
        {
            if (Player.GetComponent<PlayerMoney>().Money < Price) return;

            switch (_type) 
            {
                case ShopItemType.Health:
                    Player.GetComponent<PlayerHealth>().MaxHealth += 10;
                    Player.GetComponent<PlayerHealth>().Heal(10f);
                    break;

                case ShopItemType.Damage:
                    Player.GetComponent<PlayerAttack>().Damage += 5;
                    break;

                case ShopItemType.Speed:
                    Player.GetComponent<PlayerMovement>().MovementSpeedModifier *= 2;
                    break;
                
                case ShopItemType.Heal:
                    Player.GetComponent<PlayerHealth>().HealToFull();
                    break;
            }

            Player.GetComponent<PlayerMoney>().Money -= Price;
            Price += _shopItemInfo.PriceAddAfterBuy;
        }
    }
}