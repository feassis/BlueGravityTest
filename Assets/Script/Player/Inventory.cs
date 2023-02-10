using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Inventory
{
    private List<ProductOnInventory> items = new List<ProductOnInventory>();
    private List<EquipedItem> equipedItems;
    private List<string> namedItems = new List<string>();

    private Action OnEquipChange;

    public Inventory()
    {
        equipedItems = new List<EquipedItem>();
        equipedItems.Add(new EquipedItem(ProductType.Hat, null));
        equipedItems.Add(new EquipedItem(ProductType.Legs, null));
        equipedItems.Add(new EquipedItem(ProductType.Shirt, null));
    }

    public void AddNamedItem(string itemName)
    {
        var itemOnInventory = namedItems.Find(i => i.Equals(itemName));
        
        if (itemOnInventory != null)
        {
            return;
        }

        namedItems.Add(itemName);
    }

    public void RemoveNamedItem(string itemName)
    {
        namedItems.Remove(itemName);
    }

    public bool HasNamedItem(string itemName)
    {
        var itemOnInventory = namedItems.Find(i => i.Equals(itemName));

        if (itemOnInventory == null)
        {
            return false;
        }

        return true;
    }

    public void AddItemToInventory(ProductOnInventory product)
    {
        items.Add(product);
    }

    public List<ProductOnInventory> GetAllItensOnInventory()
    {
        return items;
    }

    public List<ProductOnInventory> GetItensWithStatus(ProductOnInventoryStatus status)
    {
        List<ProductOnInventory> products = new List<ProductOnInventory>();

        foreach (var item in items)
        {
            if(item.Status == status)
            {
                products.Add(item);
            }
        }

        return products;
    }

    public void RemoveItemFromInventory(int itemId)
    {
        var itemToRemove = items.Find(i => i.Product.Id == itemId);

        if(itemToRemove != null)
        {
            items.Remove(itemToRemove);
        }
    }

    public EquipedItem GetItemOnEquipedSlot(ProductType type)
    {
        var equipedItem = equipedItems.Find(e => e.Type == type);
        return equipedItem;
    }

    public List<EquipedItem> GetEquipedItems()
    {
        return equipedItems;
    }

    public bool IsItemEquiped(Product product)
    {
        var equipSlot = GetItemOnEquipedSlot(product.ProductType);

        if(equipSlot.Product == null)
        {
            return false;
        }

        return equipSlot.Product.Id == product.Id;
    }

    public ProductOnInventory TryGetItemById(int id)
    {
        var item = items.Find(i => i.Product.Id == id);

        if (item != null)
        {
            return item;
        }

        return null;
    }

    public void EquipItem(Product product)
    {
        var equipSlot = GetItemOnEquipedSlot(product.ProductType);

        equipSlot.Product = product;

        OnEquipChange?.Invoke();
    }

    public void RemoveEquipItem(Product product)
    {
        var equipSlot = GetItemOnEquipedSlot(product.ProductType);

        equipSlot.Product = null;
        OnEquipChange?.Invoke();
    }

    public void RegisterActionToOnEquipChange(Action action)
    {
        OnEquipChange += action;
    }

    public void DeregisterActionToOnEquipChange(Action action)
    {
        OnEquipChange -= action;
    }
}

public class EquipedItem
{
    public ProductType Type;
    public Product Product;

    public EquipedItem(ProductType type, Product item)
    {
        Type = type;
        Product = item;
    }
}
