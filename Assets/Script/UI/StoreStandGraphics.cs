using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreStandGraphics : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private ItemPreviewGraphics itemPreviewPrefab;
    [SerializeField] private ItemPreviewGraphics selectedPreviewItem;
    [SerializeField] private Transform itemPreviewHolder;
    [SerializeField] private StatsGraphics itemStats;
    [SerializeField] private TextMeshProUGUI walletAmount;
    [SerializeField] private GameObject priceGameObject;
    [SerializeField] private TextMeshProUGUI priceAmount;
    [SerializeField] private Button AddButton;
    [SerializeField] private Button RemoveButton;
    [SerializeField] private TextMeshProUGUI itemName;

    private List<ItemPreviewGraphics> itensOnSale = new List<ItemPreviewGraphics>();

    private bool addItemLock = false;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        AddButton.onClick.AddListener(OnAddButtonClicked);
        RemoveButton.onClick.AddListener(OnRemoveButtonCliked);
    }

    private void OnCloseButtonClicked()
    {
        ServiceLocator.GetService<UIManager>().CloseStoreStand();
    }
    private void OnAddButtonClicked()
    {
        if (addItemLock)
        {
            return;
        }

        addItemLock = true;
        var inventory = ServiceLocator.GetService<Inventory>();

        selectedPreviewItem.myProduct.Status = ProductOnInventoryStatus.InCart;
        inventory.AddItemToInventory(selectedPreviewItem.myProduct);

        UpdateItemsPreview();
        addItemLock = false;
    }

    private void OnRemoveButtonCliked()
    {
        var inventory = ServiceLocator.GetService<Inventory>();
        int itemId = selectedPreviewItem.myProduct.Product.Id;
        inventory.RemoveItemFromInventory(itemId);
        selectedPreviewItem.myProduct.Status = ProductOnInventoryStatus.OwnedByStore;
        var item = itensOnSale.Find(i => i.myProduct.Product.Id == itemId);

        if(item != null)
        {
            item.myProduct.Status = ProductOnInventoryStatus.OwnedByStore;
        }

        UpdateItemsPreview();
    }

    public void Open(ProductType productType)
    {
        gameObject.SetActive(true);
        addItemLock = false;

        var productService = ServiceLocator.GetService<ProductService>();

        var availableProducts = productService.GetProductsOfAType(productType);

        var inventory = ServiceLocator.GetService<Inventory>();

        foreach (var product in availableProducts)
        {
            var itemPreview = Instantiate(itemPreviewPrefab, itemPreviewHolder);
            ProductOnInventory currentProduct;

            var itemOnInventory = inventory.TryGetItemById(product.Id);

            if(itemOnInventory == null)
            {
                currentProduct = new ProductOnInventory(product, ProductOnInventoryStatus.OwnedByStore);
            }
            else
            {
                currentProduct = new ProductOnInventory(product, itemOnInventory.Status);
            }
            
            itemPreview.Setup(currentProduct, this);
            itensOnSale.Add(itemPreview);
        }

        walletAmount.text = ServiceLocator.GetService<Wallet>().GetMoneyAmount().ToString();
    }

    private void UpdateItemsPreview()
    {
        selectedPreviewItem.UpdateItemPreview();

        foreach (var item in itensOnSale)
        {
            item.UpdateItemPreview();
        }

        ChooseButtomToShow();
    }

    private void ChooseButtomToShow()
    {
        var inventory = ServiceLocator.GetService<Inventory>();
        var desiredItemOnInventory = inventory.TryGetItemById(selectedPreviewItem.myProduct.Product.Id);

        if(desiredItemOnInventory == null)
        {
            AddButton.gameObject.SetActive(true);
            RemoveButton.gameObject.SetActive(false);
        }
        else if (desiredItemOnInventory.Status == ProductOnInventoryStatus.InCart)
        {
            AddButton.gameObject.SetActive(false);
            RemoveButton.gameObject.SetActive(true);
        }
        else 
        {
            AddButton.gameObject.SetActive(false);
            RemoveButton.gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        foreach (var item in itensOnSale)
        {
            Destroy(item.gameObject);
        }

        itensOnSale.Clear();

        ToggleSelectedItensDetails(false);
        gameObject.SetActive(false);
    }

    private void ToggleSelectedItensDetails(bool isActive)
    {
        if (!isActive)
        {
            AddButton.gameObject.SetActive(false);
            RemoveButton.gameObject.SetActive(false);
        }

        if (selectedPreviewItem.myProduct != null)
        {
            itemName.text = selectedPreviewItem.myProduct.Product.ItemName;
        }
        
        itemName.gameObject.SetActive(isActive);
        selectedPreviewItem.gameObject.SetActive(isActive);
        itemStats.gameObject.SetActive(isActive);
        priceGameObject.SetActive(isActive);
    }

    public void SelectItem(ProductOnInventory product)
    {
        priceAmount.text = product.Product.Price.ToString();
        selectedPreviewItem.Setup(product, this);
        itemStats.Setup(product.Product);
        ToggleSelectedItensDetails(true);

        ChooseButtomToShow();
        UpdateItemsPreview();
    }
}
