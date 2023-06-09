﻿using System;

namespace Domain
{
    public class Stock
    {
        private Stock() { }

        public Stock(int storeId, int productId, int quantity) : this()
        {
            StoreId = storeId;
            ProductId = productId;
            Quantity = quantity;
        }

        public int StoreId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public byte[] RowVersion { get; private set; }

        public virtual Product Product { get; private set; } = null!;
        public virtual Store Store { get; private set; } = null!;

        public void TakeProduct(int quantity)
        {
            Quantity -= quantity;
            return;
        }

        public void RollbackQuantity(int quantity)
        {
            Quantity = quantity;
            return;
        }
    }
}
