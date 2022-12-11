using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    public int money=0;
	public Text moneyText;
	public Action<int> onMoneyChanged;
	
	public void addMoney(int n) {
		money+=n;
		onMoneyChanged?.Invoke(money);
		//moneyText.text=money.ToString();
	}
}
