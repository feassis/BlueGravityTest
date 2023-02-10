using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product Config", menuName ="Configs/Product Config")]
public class ProductConfig : ScriptableObject
{
    public List<Product> Products;
}