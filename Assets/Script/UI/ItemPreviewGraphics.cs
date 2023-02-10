using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreviewGraphics : MonoBehaviour
{
    [SerializeField] private Image itemPreviewImage;
    [SerializeField] private Image itemOnCartImage;
    [SerializeField] private Image itemOnPlayerInventoryImage;
    [SerializeField] private Button previewButton;

    public ProductOnInventory myProduct;
    private StoreStandGraphics myStoreGraphics;

    private void Awake()
    {
        previewButton.onClick.AddListener(OnPreviewButtonClicked);
    }

    public void Setup(ProductOnInventory product, StoreStandGraphics storeGraphics)
    {
        myProduct = product;
        itemPreviewImage.sprite = product.Product.ProductImage;
        myStoreGraphics = storeGraphics;
        UpdateItemPreview();
    }

    public void UpdateItemPreview()
    {
        itemOnCartImage.gameObject.SetActive(false);
        itemOnPlayerInventoryImage.gameObject.SetActive(false);

        if(myProduct.Status == ProductOnInventoryStatus.InCart)
        {
            itemOnCartImage.gameObject.SetActive(true);
        }
        else if (myProduct.Status == ProductOnInventoryStatus.OwnedByPlayer)
        {
            itemOnPlayerInventoryImage.gameObject.SetActive(true);
        }
    }

    private void OnPreviewButtonClicked()
    {
        myStoreGraphics.SelectItem(myProduct);
    } 
}
