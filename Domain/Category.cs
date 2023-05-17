using System;
using System.Collections.Generic;

namespace Domain;

public class Category
{
    public Category(int categoryId, string categoryName)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
        
    }

    public int CategoryId { get; private set; }

    public string CategoryName { get; private set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
