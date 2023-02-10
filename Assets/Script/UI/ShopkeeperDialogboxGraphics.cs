using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopkeeperDialogboxGraphics : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI buyText;

    private const string notEnoughMoneyString = "Sorry! But you don't seen to have enough money.";
    private const string sellStringString = "Hi! I want to sell some clothes. ({0}% of value)";
    private const string BuyStringString = "I want to buy these";

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        sellButton.onClick.AddListener(OnSellButtonClicked);
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        sellText.text = string.Format(sellStringString, Mathf.RoundToInt(GameConstants.SellPercentage * 100));
        buyText.text = BuyStringString;
    }

    private void OnCloseButtonClicked()
    {
        Close();
    }

    private void OnSellButtonClicked()
    {
        ServiceLocator.GetService<UIManager>().OpenInventory(true);
    }

    private int GetCartPrice()
    {
        var inventory = ServiceLocator.GetService<Inventory>();

        var cartItems = inventory.GetItensWithStatus(ProductOnInventoryStatus.InCart);
        int price = 0;

        foreach (var item in cartItems)
        {
            price += item.Product.Price;
        }

        return price;
    }

    private void OnBuyButtonClicked()
    {
        var inventory = ServiceLocator.GetService<Inventory>();

        var cartItems = inventory.GetItensWithStatus(ProductOnInventoryStatus.InCart);

        int price = GetCartPrice();

        var wallet = ServiceLocator.GetService<Wallet>();

        if(wallet.GetMoneyAmount() >= price)
        {
            foreach (var item in cartItems)
            {
                inventory.RemoveItemFromInventory(item.Product.Id);
                item.Status = ProductOnInventoryStatus.OwnedByPlayer;
                inventory.AddItemToInventory(item);
            }

            wallet.SubtractMoney(price);

            Close();
            return;
        }

        dialogText.text = notEnoughMoneyString;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);

        var inventory = ServiceLocator.GetService<Inventory>();
        var items = inventory.GetItensWithStatus(ProductOnInventoryStatus.InCart);

        if (items == null || items.Count == 0)
        {
            return;
        }

        int randomNumber = Random.Range(0, items.Count);

        dialogText.text = items[randomNumber].Product.ShopkeeperComment;
    }
}
