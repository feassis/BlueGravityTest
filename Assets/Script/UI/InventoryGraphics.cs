using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryGraphics : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private ItemInventoryPreviewGraphics itemPreviewPrefab;
    [SerializeField] private ItemInventoryPreviewGraphics selectedPreview;
    [SerializeField] private Transform displayHolder;
    [SerializeField] private StatsGraphics itemStats;
    [SerializeField] private StatsGraphics currentStats;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private List<EquipSlot> equipmentSlots = new List<EquipSlot>();
    [SerializeField] private TextMeshProUGUI itemName;

    private bool isSellingEnabled;

    [Serializable]
    private struct EquipSlot
    {
        public ProductType Type;
        public EquipmentSlotGraphics EquipSlotGraphics;
    }

    private List<ItemInventoryPreviewGraphics> itemsOnInventory = new List<ItemInventoryPreviewGraphics>();

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        equipButton.onClick.AddListener(OnEquipButtonClicked);
        removeButton.onClick.AddListener(OnRemoveButtonClicked);
        sellButton.onClick.AddListener(OnSellButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        ServiceLocator.GetService<UIManager>().CloseInventory();
    }

    private void OnEquipButtonClicked()
    {
        var inventory = ServiceLocator.GetService<Inventory>();

        inventory.EquipItem(selectedPreview.myProduct.Product);
    }

    private void OnRemoveButtonClicked()
    {
        var inventory = ServiceLocator.GetService<Inventory>();

        inventory.RemoveEquipItem(selectedPreview.myProduct.Product);
    }

    private void OnSellButtonClicked()
    {
        if(selectedPreview.myProduct.Product == null)
        {
            return;
        }

        int marketPriveValue = Mathf.RoundToInt(selectedPreview.myProduct.Product.Price * GameConstants.SellPercentage);
        var inventory = ServiceLocator.GetService<Inventory>();

        if(inventory.IsItemEquiped(selectedPreview.myProduct.Product))
        {
            inventory.RemoveEquipItem(selectedPreview.myProduct.Product);
        }

        var iconToDestroyed = itemsOnInventory.Find(i => i.myProduct.Product.Id == selectedPreview.myProduct.Product.Id);
        itemsOnInventory.Remove(iconToDestroyed);
        Destroy(iconToDestroyed.gameObject);

        inventory.RemoveItemFromInventory(selectedPreview.myProduct.Product.Id);

        ServiceLocator.GetService<Wallet>().AddMoney(marketPriveValue);

        ToggleSelectedItemsDetails(false);
        sellButton.gameObject.SetActive(false);
        selectedPreview.myProduct.Product = null;
    }

    private void RefreshItems()
    {
        foreach (var item in itemsOnInventory)
        {
            item.UpdateItemPreview();
        }

        var inventory = ServiceLocator.GetService<Inventory>();
        foreach (var slot in equipmentSlots)
        {
            var equipmentOnSlot = inventory.GetItemOnEquipedSlot(slot.Type);

            slot.EquipSlotGraphics.Setup(equipmentOnSlot.Product);
        }

        var playerController = ServiceLocator.GetService<PlayerController>();
        var stats= playerController.GetPlayerStats();
        currentStats.Setup(stats.badassery, stats.coolness, stats.cuteness);

        ChooseWhichButtonToShow();
    }

    private void ChooseWhichButtonToShow()
    {
        equipButton.gameObject.SetActive(false);
        removeButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
        
        if(selectedPreview.myProduct == null)
        {
            return;
        }

        if (isSellingEnabled)
        {
            sellButton.gameObject.SetActive(true);
            return;
        }

        var inventory = ServiceLocator.GetService<Inventory>();

        bool isItemEquiped = inventory.IsItemEquiped(selectedPreview.myProduct.Product);
        equipButton.gameObject.SetActive(!isItemEquiped);
        removeButton.gameObject.SetActive(isItemEquiped);
    }

    public void Open(bool isSellingEnabled)
    {
        gameObject.SetActive(true);
        this.isSellingEnabled = isSellingEnabled;

        var inventory = ServiceLocator.GetService<Inventory>();


        List<ProductOnInventory> items;

        if (isSellingEnabled)
        {
            items = inventory.GetItensWithStatus(ProductOnInventoryStatus.OwnedByPlayer);
        }
        else
        {
            items = inventory.GetAllItensOnInventory();
        }

        foreach (var item in items)
        {
            var displayItem = Instantiate(itemPreviewPrefab, displayHolder);

            displayItem.Setup(item, this);

            itemsOnInventory.Add(displayItem);
        }

        inventory.RegisterActionToOnEquipChange(RefreshItems);
        RefreshItems();
    }

    public void Close()
    {
        foreach (var item in itemsOnInventory)
        {
            Destroy(item.gameObject);
        }

        itemsOnInventory.Clear();

        var inventory = ServiceLocator.GetService<Inventory>();
        inventory.DeregisterActionToOnEquipChange(RefreshItems);

        ToggleSelectedItemsDetails(false);
        equipButton.gameObject.SetActive(false);
        removeButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }


    public void SelectItem(ProductOnInventory product)
    {
        selectedPreview.Setup(product, this);
        UpdateStats();
        ToggleSelectedItemsDetails(true);
    }

    private void ToggleSelectedItemsDetails(bool isActive)
    {
        if (isActive)
        {
            ChooseWhichButtonToShow();
            itemName.text = selectedPreview.myProduct.Product.ItemName; 
        }

        itemName.gameObject.SetActive(isActive);
        itemStats.gameObject.SetActive(isActive);
        selectedPreview.gameObject.SetActive(isActive);
    }

    private void UpdateStats()
    {
        itemStats.Setup(selectedPreview.myProduct.Product);
    }
}

