﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderContainer
    {
        private OrderContainer()
        {
            OrderItems = new List<OrderItem>();
            Errors = new Dictionary<OrderItem, string>(); // Initialize Errors as a dictionary
        }

        public OrderContainer(int staffId, int storeId, List<Stock> stocks, List<Product> products) : this()
        {
            StaffId = staffId;
            StoreId = storeId;
            Stocks = stocks;
            Products = products;
           
        }

        public int StaffId { get; private set; }
        public int StoreId { get; private set; }
        public bool IsValid { get; private set; }
        public List<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
        public List<Stock> Stocks { get; private set; } = new List<Stock>();
        public List<Product> Products { get; private set; } = new List<Product>();
        public Dictionary<OrderItem, string> Errors { get; private set; } // Change Errors to a dictionary

        public void AddOrderItem(int itemId, int productId, int quantity, decimal discount)
        {
            decimal listPrice = Products.FirstOrDefault(x => x.ProductId == productId).ListPrice;
            
            OrderItem orderItem = new OrderItem(itemId, productId, quantity, discount, listPrice);
            OrderItems.Add(orderItem);
        }

        public void ClearErrors()
        {
            Errors = new Dictionary<OrderItem, string>();
        }

        public void SubstituteProduct(List<int> productIds) 
        {
            ClearErrors();
            var matchingOrderItems = OrderItems.Where(x => productIds.Contains(x.ProductId)).ToList();

            
            foreach (var orderItem in matchingOrderItems)
            {
                Product product = Products.Where(x => x.ProductId == orderItem.ProductId).FirstOrDefault();

                var potentialSubstitutions = Products.Where(x => x.ProductId != orderItem.ProductId &&
                                                      ((x.BrandId == product.BrandId && x.ProductName == product.ProductName) ||
                                                       (x.BrandId == product.BrandId) ||
                                                       (x.ProductName == product.ProductName) ||
                                                       (x.CategoryId == product.CategoryId) ||
                                                       (x.ModelYear == product.ModelYear)))
                                          .OrderByDescending(x =>
                                          (x.BrandId == product.BrandId && x.ProductName == product.ProductName) ? 3 :
                                          (x.BrandId == product.BrandId && x.CategoryId == product.CategoryId && x.ModelYear == product.ModelYear) ? 3 :
                                          (x.BrandId == product.BrandId && x.CategoryId == product.CategoryId) ? 2 :
                                          (x.CategoryId == product.CategoryId && x.ModelYear == product.ModelYear) ? 1 :
                                          (x.CategoryId == product.CategoryId) ? 1 :
                                          (x.BrandId == product.BrandId) ? 1 :
                                          (x.ModelYear == product.ModelYear) ? 1 :
                                          0)
                                          .ToList();
                foreach(var potentialSubstitution in potentialSubstitutions)
                {

                    Stock stock = Stocks.FirstOrDefault(x => x.ProductId == potentialSubstitution.ProductId && x.Quantity >= orderItem.Quantity);
                    bool substitutionAlreadyExists = OrderItems.Any(item => item.ProductId == potentialSubstitution.ProductId);

                    if (stock != null && !substitutionAlreadyExists)
                    {
                        orderItem.SubstituteProduct(potentialSubstitution.ProductId);
                        break;
                    }
                }

            }
        }

        public void Validate()
        {
            foreach (OrderItem orderItem in OrderItems)
            {
                Stock? stock = Stocks.FirstOrDefault(x => x.ProductId == orderItem.ProductId && x.Quantity >= orderItem.Quantity)
                ?? Stocks.FirstOrDefault(x => x.ProductId == orderItem.ProductId);

                if (orderItem.Quantity < 1)
                {
                    if (Errors.ContainsKey(orderItem))
                    {
                        Errors[orderItem] += $", The asking quantity for {orderItem.ProductId} is 0 or negative";
                    }
                    else
                    {
                        Errors.Add(orderItem, $"The asking quantity for {orderItem.ProductId} is 0 or negative");
                    }
                }

                if (stock == null)
                {
                    if (Errors.ContainsKey(orderItem))
                    {
                        Errors[orderItem] += $", No matching product in the stock list for {orderItem.ProductId}";
                    }
                    else
                    {
                        Errors.Add(orderItem, $"No matching product in the stock list for {orderItem.ProductId}");
                    }
                    continue;
                }
               
                if (stock.Quantity < orderItem.Quantity)
                {
                    
                    
                    if (Errors.ContainsKey(orderItem))
                    {
                        Errors[orderItem] += $", Quantity in the stock is too low for {orderItem.ProductId}";
                    }
                    else
                    {
                        Errors.Add(orderItem, $"Quantity in the stock is too low for {orderItem.ProductId}");
                    }
                }
            }
            IsValid = Errors.Count == 0;
        }

    }
}
