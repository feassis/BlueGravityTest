using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryPreviewGraphics : MonoBehaviour
{
    [SerializeField] private Image itemPreviewImage;
    [SerializeField] private Image itemOnCartImage;
    [SerializeField] private Image itemEquipedOnPlayerInventoryImage;
    [SerializeField] private Button previewButton;

    public ProductOnInventory myProduct;
    private InventoryGraphics myInventory;

    private void Awake()
    {
        previewButton.onClick.AddListener(OnPreviewButtonClicked);
    }

    public void Setup(ProductOnInventory product, InventoryGraphics inventoryGraphics)
    {
        myProduct = product;
        itemPreviewImage.sprite = product.Product.ProductImage;
        myInventory = inventoryGraphics;
        UpdateItemPreview();
    }

    public void UpdateItemPreview()
    {
        itemOnCartImage.gameObject.SetActive(false);
        itemEquipedOnPlayerInventoryImage.gameObject.SetActive(false);

        if (myProduct.Status == ProductOnInventoryStatus.InCart)
        {
            itemOnCartImage.gameObject.SetActive(true);
        }

        var inventory = ServiceLocator.GetService<Inventory>();
        if (inventory.IsItemEquiped(myProduct.Product))
        {
            itemEquipedOnPlayerInventoryImage.gameObject.SetActive(true);
        }
    }

    private void OnPreviewButtonClicked()
    {
        myInventory.SelectItem(myProduct);
    }
}
