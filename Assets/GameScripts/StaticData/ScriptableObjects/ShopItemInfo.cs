using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShopItemInfo", menuName = "StaticData/ShopItemInfo", order = 0)]
    public class ShopItemInfo : ScriptableObject
    {
        public ShopItemType Type; 
        public int Price;
        public int PriceAddAfterBuy;
    }
}