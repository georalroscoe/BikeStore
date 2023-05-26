using System;
using System.Collections.Generic;

namespace Domain;

public class Brand
{
    private Brand() {
    Products = new List<Product>();
    }
    public Brand(int brandId, string brandName) : this()
    {
        BrandId= brandId;
        BrandName = brandName;

    }

    public int BrandId { get; private set; }

    public string BrandName { get; private set; } = null!;

    public virtual ICollection<Product> Products { get; private set; } = new List<Product>();

    public void AddProduct(int productId, string productName, int categoryId, short modelYear, decimal listPrice)
    {
        Product product = new Product(productId, BrandId, productName, categoryId, modelYear, listPrice);
        Products.Add(product);
        return;
    }
}
