using NaughtyAttributes;
using System;
using UnityEngine;

[Serializable]
public class Product
{
    public int Id;
    public string ItemName;
    public string ShopkeeperComment;
    public int Badassery;
    public int Coolness;
    public int Cuteness;
    public int Price;
    public ProductType ProductType;
    [ShowAssetPreview] public Sprite ProductImage;
}
