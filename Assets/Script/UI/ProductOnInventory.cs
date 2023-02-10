public class ProductOnInventory
{
    public Product Product;
    public ProductOnInventoryStatus Status;

    public ProductOnInventory(Product product, ProductOnInventoryStatus status)
    {
        Product = product;
        Status = status;
    }
}
