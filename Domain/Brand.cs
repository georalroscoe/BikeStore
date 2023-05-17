using System;
using System.Collections.Generic;

namespace Domain;

public class Brand
{

    public Brand(int brandId, string brandName)
    {
        BrandId = brandId;
        BrandName = brandName;
       
    }

    public int BrandId { get; private set; }

    public string BrandName { get; private set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
