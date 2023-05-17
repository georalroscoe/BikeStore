using System;
using System.Collections.Generic;

namespace Domain;

public class Product
{
    private Product() {
    OrderItems = new List<OrderItem>();
    Stocks = new List<Stock>();
    }
    //public Product(int productId, string productName, int brandId, int categoryId, short modelYear, decimal listPrice)
    //{
    //    ProductId = productId;
    //    ProductName = productName;
    //    BrandId = brandId;
    //    CategoryId = categoryId;
    //    ModelYear = modelYear;
    //    ListPrice = listPrice;
       
    //}

    public int ProductId { get; private set; }

    public string ProductName { get; private set; } = null!;

    public int BrandId { get; private set; }

    public int CategoryId { get; private set; }

    public short ModelYear { get; private set; }

    public decimal ListPrice { get; private set; }

    public virtual Brand Brand { get; private set; } = null!;

    public virtual Category Category { get; private set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    public virtual ICollection<Stock> Stocks { get; private set; } = new List<Stock>();
}
