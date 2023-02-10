using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCartPreviewGraphics : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button removeButton;

    private Product product;
    private CartGraphics cart;

    private void Awake()
    {
        removeButton.onClick.AddListener(OnRemoveButtonClicked);
    }

    private void OnRemoveButtonClicked()
    {
        cart.RemoveItemFromCart(this, product);
    }

    public void Setup(Product product, CartGraphics cartGraphics)
    {
        this.product = product;
        itemIcon.sprite = product.ProductImage;
        priceText.text = product.Price.ToString();
        cart = cartGraphics;
    }

    public int GetProductPrice()
    {
        return product.Price;
    }
}
