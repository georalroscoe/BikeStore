using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DataAccess;
using DataAccess.Mapping;
using DataAccess.Repositories;
using Microsoft.Identity.Client;
using System.Diagnostics;
using Application.Interfaces;
using Dtos;

using System.Xml;


namespace Application { 
    public class CreateOrder : ICreateOrders
    {

        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Customer> _customerRepo;
        private readonly IGenericRepository<Staff> _staffRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<Stock> _stockRepo;

        public CreateOrder (IUnitOfWork uow, IGenericRepository<Customer> customer, IGenericRepository<Staff> staff, IGenericRepository<Product> product, IGenericRepository<Stock> stock) { 
            _uow = uow; 
            _customerRepo = customer;
            _staffRepo = staff;
            _productRepo = product;
            _stockRepo = stock;
        }

        public void OrderCreator(OrderDto orderDto)
        {
            var staff = _staffRepo.GetById(orderDto.StaffId);
            var customer = _customerRepo.GetById(orderDto.CustomerId);
            
            if (customer == null)
            {
                throw new Exception("no customer by this ID");
                //create a new customer, would need to go back to api and request more info from user
            }

            Order newOrder = customer.CreateOrder(staff.StaffId, staff.StoreId);
            foreach (OrderProductDto orderProductDto in orderDto.Products.ToList())
            {
                Stock? stock = _stockRepo.Get(x => x.StoreId == staff.StoreId && x.ProductId == orderProductDto.ProductId).FirstOrDefault();
                if (stock == null )
                {
                    throw new Exception("Product does not exist in stock list");
                }
                
                decimal listPrice = stock.Product.ListPrice;
                newOrder.FillOrder(stock, orderProductDto.ItemId, orderProductDto.ProductId, listPrice, orderProductDto.Quantity, orderProductDto.Discount);

                
            }
            _uow.Save();
           
            //productid, quantity, discount, itemid passed to a list on a dto with staff and customerid
            //set order date, shipping date 5 business days from that date, shipped date might have to be dealt with
            //get store from the staff
            
            //check stock at that store,
            //take away quantity from stock
            //check if there is a customer, if not make one

            //
        }



    }
}