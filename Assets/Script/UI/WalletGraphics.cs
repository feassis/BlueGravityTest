using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tools;

public class WalletGraphics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyAmount;
    [SerializeField] private Transform moneyChangeHolder;
    [SerializeField] private MonetaryChangeGraphics monetaryChangePrefab;

    private void Awake()
    {
        ServiceLocator.RegisterService<WalletGraphics>(this);
    }

    private void Start()
    {
        var wallet = ServiceLocator.GetService<Wallet>();
        wallet.RegisterActionToWalletValueChange(RefreshWallet);
        RefreshWallet(wallet.GetMoneyAmount(), 0);
    }

    private void OnDestroy()
    {
        ServiceLocator.DeregisterService<WalletGraphics>();
    }

    private void MoneyChangeAnimation(int amount)
    {
        if(amount == 0)
        {
            return;
        }

        StartCoroutine(CallMoneyChangeAnimation(amount));
    }

    private IEnumerator CallMoneyChangeAnimation(int amount)
    {
        var moneyChange = Instantiate(monetaryChangePrefab, moneyChangeHolder);
        moneyChange.Setup(amount);
        yield return new WaitForSeconds(1.1f);

        Destroy(moneyChange.gameObject);
    }

    private void RefreshWallet(int walletAmount, int changeValue)
    {
        moneyAmount.text = walletAmount.ToString();

        MoneyChangeAnimation(changeValue);
    }
}
