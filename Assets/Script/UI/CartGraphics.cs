using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartGraphics : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private ItemCartPreviewGraphics itemCartPreviewPrefab;
    [SerializeField] private Transform scrollviewContent;
    [SerializeField] private TextMeshProUGUI cartAmountText;

    private List<ItemCartPreviewGraphics> itemsOnCart = new List<ItemCartPreviewGraphics>();

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        ServiceLocator.GetService<UIManager>().CloseCart();
    }

    public void Open()
    {
        gameObject.SetActive(true);

        var inventory = ServiceLocator.GetService<Inventory>();
        List<ProductOnInventory> inventoryCartProducts = inventory.GetItensWithStatus(ProductOnInventoryStatus.InCart);

        foreach (var product in inventoryCartProducts)
        {
            var cartItem = Instantiate(itemCartPreviewPrefab, scrollviewContent);
            cartItem.Setup(product.Product, this);

            itemsOnCart.Add(cartItem);
        }

        UpdateCartValue();
    }

    public void Close()
    {
        foreach (var item in itemsOnCart)
        {
            Destroy(item.gameObject);
        }

        itemsOnCart.Clear();

        gameObject.SetActive(false);
    }

    private void UpdateCartValue()
    {
        int totalCartValue = 0;
        foreach (ItemCartPreviewGraphics item in itemsOnCart)
        {
            totalCartValue += item.GetProductPrice();
        }

        cartAmountText.text = totalCartValue.ToString();

        var wallet = ServiceLocator.GetService<Wallet>();
        if(wallet.GetMoneyAmount() >= totalCartValue)
        {
            cartAmountText.color = Color.green;
        }
        else
        {
            cartAmountText.color = Color.red;
        }
    }

    public void RemoveItemFromCart(ItemCartPreviewGraphics previewCartItem, Product productToRemove)
    {
        var invetory = ServiceLocator.GetService<Inventory>();
        invetory.RemoveItemFromInventory(productToRemove.Id);

        itemsOnCart.Remove(previewCartItem);
        Destroy(previewCartItem.gameObject);

        UpdateCartValue();
    }
}
