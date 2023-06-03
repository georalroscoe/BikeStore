using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain;

public class Stock
{
    private Stock() { }
    public Stock(int storeId, int productId, int quantity) : this()
    {
        StoreId = storeId;
        ProductId = productId;
        Quantity = quantity;
        TimeStamp = GenerateTimeStamp();

    }

    public int StoreId { get; private set; }

    public int ProductId { get; private set; }

    public int Quantity { get; private set; }
    
    public byte[] TimeStamp { get; private set; }

    public virtual Product Product { get; private set; } = null!;

    public virtual Store Store { get; private set; } = null!;

    public bool TakeProduct(byte[] currentTimeStamp, int quantity)
    {
        if (!IsTimestampValid(currentTimeStamp))
        {
            
            throw new Exception("Concurrency conflict occurred. Stock has been modified by another transaction.");
        }
        if (quantity <= Quantity)
        {
            Quantity -= quantity;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsTimestampValid(byte[] currentTimestamp)
    {
        return StructuralComparisons.StructuralEqualityComparer.Equals(currentTimestamp, TimeStamp);
    }

    private byte[] GenerateTimeStamp()
    {
        return BitConverter.GetBytes(DateTime.UtcNow.Ticks);
    }
}
