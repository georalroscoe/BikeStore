using System;
using System.Collections.Generic;

namespace Domain;

public class Category
{
    private Category() {
    Products = new List<Product>();
    }
    public Category(string categoryName) : this()
    {
        
        CategoryName = categoryName;

    }

    public int CategoryId { get; private set; }

    public string CategoryName { get; private set; } = null!;

    public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
}
