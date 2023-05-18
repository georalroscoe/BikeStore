﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class OrderProductDto
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
    }
}
