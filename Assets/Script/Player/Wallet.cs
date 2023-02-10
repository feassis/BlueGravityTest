using System;
using UnityEngine;

public class Wallet
{
    private int MoneyAmount;

    private Action<int, int> OnWalletvalueChange;

    public Wallet(int initialAmount)
    {
        MoneyAmount = initialAmount;
    }

    public int GetMoneyAmount()
    {
        return MoneyAmount;
    }

    public bool HasAmount(int amount)
    {
        return amount >= MoneyAmount;
    }

    public void AddMoney(int amount)
    {
        MoneyAmount += amount;
        OnWalletvalueChange?.Invoke(MoneyAmount, amount);
    }

    public void SubtractMoney(int amount)
    {
        MoneyAmount = Mathf.Clamp(MoneyAmount - amount, 0, int.MaxValue);
        OnWalletvalueChange?.Invoke(MoneyAmount, -amount);
    }

    public void RegisterActionToWalletValueChange(Action<int, int> action)
    {
        OnWalletvalueChange += action;
    }

    public void DeregisterActionToWalletValueChange(Action<int, int> action)
    {
        OnWalletvalueChange -= action;
    }
}
