using System;
using UnityEngine;

namespace GameScripts.Logic.Units.Player
{
	public class PlayerMoney : MonoBehaviour
	{
		public Action<int> OnMoneyChanged;
		private int _money;
		public int Money
		{
			get => _money;
			set
			{
				OnMoneyChanged?.Invoke(value);
				_money = value;
			}
		}

		private void OnDisable()
		{
			OnMoneyChanged = null;
		}
	}
}
