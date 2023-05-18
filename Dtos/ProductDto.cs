using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class ProductDto
    {
        public int BrandId { get; set; }
        public string ProductName { get; set; }

        public int CategoryId { get; set; }

        public short ModelYear { get; set; }

        public decimal ListPrice { get; set; }
    }
}
