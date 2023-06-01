using System;
using System.Collections.Generic;

namespace Domain;

public class Brand
{
    private Brand() {
    Products = new List<Product>();
    }
    public Brand(string brandName) : this()
    {
       
        BrandName = brandName;

    }

    public int BrandId { get; private set; }

    public string BrandName { get; private set; } = null!;

    public virtual ICollection<Product> Products { get; private set; } = new List<Product>();

    public Product AddProduct(string productName, int categoryId, short modelYear, decimal listPrice)
    {
        Product product = new Product(BrandId, productName, categoryId, modelYear, listPrice);
        Products.Add(product);
        
        return product;
    }
}
