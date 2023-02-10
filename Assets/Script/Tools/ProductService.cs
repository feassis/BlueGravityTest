using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class ProductService : MonoBehaviour
{
    [SerializeField] private ProductConfig productConfig;

    private void Awake()
    {
        ServiceLocator.RegisterService<ProductService>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.DeregisterService<ProductService>();
    }

    public List<Product> GetProductsOfAType(ProductType type)
    {
        List<Product> productList = new List<Product>();

        foreach (var product in productConfig.Products)
        {
            if(product.ProductType == type)
            {
                productList.Add(product);
            }     
        }

        return productList;
    }
}
