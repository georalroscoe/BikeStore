using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain;

public class Order
{
    public static byte ValidOrder = 1;

    private Order()
    {
        OrderItems = new List<OrderItem>();
    }


    public Order( byte orderStatus, int storeId, int staffId, int customerId) : this()
    {
        OrderDate = DateTime.Now;
        RequiredDate = OrderDate.AddDays(5);
        OrderStatus = orderStatus;
        StoreId = storeId;
        StaffId = staffId;
        CustomerId = customerId;


    }

    //public bool FilOrder(byte[] currentTimeStamp, Stock stock, int itemId, int productId,  int quantity, decimal discount, decimal listPrice)
    //{
    //   bool validRetrieval = stock.TakeProduct(currentTimeStamp, quantity);
      
    //    OrderItem orderItem = new OrderItem(itemId, productId, quantity,  discount, listPrice);
    //    OrderItems.Add(orderItem);
        
    //    return validRetrieval;
    //}

    public void FillOrder(List<Stock> stocks, List<OrderItem> orderItems)
    {
        foreach(var orderItem in orderItems)
        {
            var stock = stocks.Where(x => x.ProductId == orderItem.ProductId && x.Quantity >= orderItem.Quantity).FirstOrDefault();
            stock.TakeProduct(orderItem.Quantity);
            OrderItems.Add(orderItem);
            //merge order items with the same productid or find diff product to prevent null errpr
        } 
    }


    //restful APIs, naming conventions, object names use nouns, controllers called orders, controller says order

    //    in application layer class called - do it the other way round 
    //look up and read about concurrency in databases 
    //optimistic and pessimistic concurrency - genreally go with optimistic. 
    //when fetching info from a store
    //    method in linq called contains (CallConvThiscall is AlterSequenceOperation a problem). 
    //look up hashing

    //create a domain in the validation domain object and put the logic for tha tvalidation in the domain objec , stock levels for all
    //the diff products in the domain object, you want the store, all of your orders, create all orders and put them in validaiton object,
    //check if its valid , have isvalid property on there which will return true if the list of problems is empty 

    public int OrderId { get; private set; }
    //on the applciaiton side check if its valid and if it isnt reutnr all the lsit of problems in the dto 
    //could do all the validaiton in the application 

    public int? CustomerId { get; private set; }

    public byte OrderStatus { get; private set; }

    public DateTime OrderDate { get; private set; }

    public DateTime RequiredDate { get; private set; }

    public DateTime? ShippedDate { get; private set; }

    public int StoreId { get; private set; }

    public int StaffId { get; private set; }

    public virtual Customer? Customer { get; private set; }

    public virtual ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    public virtual Staff Staff { get; private set; } = null!;

    public virtual Store Store { get; private set; } = null!;

    
}
