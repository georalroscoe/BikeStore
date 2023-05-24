using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Validation
    {

        private Validation()
        {
            OrderItems = new List<OrderItem>();
        }

        public Validation(int storeId) : this()
        {

            StoreId = storeId;
        }

        public int StoreId { get; private set; }

        public bool IsValid { get; private set; }

        public List<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        public List<Stock> Stocks { get; private set; } = new List<Stock>();

        public List<string> Errors { get; private set; }

        public void AddOrderItem(Stock stock, int itemId, int productId, int quantity, decimal discount)
        {
            OrderItem orderItem = new OrderItem(itemId, productId, quantity, discount);
            OrderItems.Add(orderItem);
            Stocks.Add(stock);
            return;
        }


        public void isValid()
        {
            foreach (var item in OrderItems)
            {
                if (item.Quantity < 1)
                {
                    Errors.Add($"The asking quantity for {item.ProductId} is 0 or negative ");
                }
                int? stockQuantity = Stocks.FirstOrDefault(x => x.ProductId == item.ProductId).Quantity;
                if (stockQuantity != null)
                {
                    Errors.Add($"No matching product in the stock list for {item.ProductId}");
                }
                if (stockQuantity < item.Quantity)
                {
                    Errors.Add($"Quantity in the stock is too low for {item.ProductId}");
                }
                if (item.Product == null)
                {
                    Errors.Add($"There is no {item.ProductId} product");
                }

            }
            if (Errors.Count == 0)
            {
                IsValid = true;
            }
        }
        //create a domain in the validation domain object and put the logic for tha tvalidation in the domain objec , stock levels for all
        //the diff products in the domain object, you want the store, all of your orders, create all orders and put them in validaiton object,
        //check if its valid , have isvalid property on there which will return true if the list of problems is empty 

        /* is the quantitiy valid
         * is there stock
         * does the product exist
         * is the store id valid
         */
    }
}
