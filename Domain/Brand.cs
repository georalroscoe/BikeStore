using System;
using System.Collections.Generic;

namespace Domain;

public class Brand
{
    private Brand() {
    Products = new List<Product>();
    }
    //public Brand(int brandId, string brandName)
    //{
    //    BrandId = brandId;
    //    BrandName = brandName;
       
    //}

    public int BrandId { get; private set; }

    public string BrandName { get; private set; } = null!;

    public virtual ICollection<Product> Products { get; private set; } = new List<Product>();

    public void CreateNewProduct(string productName, int categoryId, short modelYear, decimal listPrice)
    {
        Product product = new Product(productName, categoryId, modelYear, listPrice);
        Products.Add(product);
        return;
    }
}
