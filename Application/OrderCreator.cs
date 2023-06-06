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
using Microsoft.EntityFrameworkCore;

namespace Application
{ 
    public class OrderCreator : ICreateOrders
    {

        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Customer> _customerRepo;
        private readonly IGenericRepository<Staff> _staffRepo;
        private readonly IGenericRepository<Stock> _stockRepo;
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<OrderItem> _orderItemRepo;
        private readonly IGenericRepository<Product> _productRepo;


        public OrderCreator (IUnitOfWork uow, IGenericRepository<Customer> customer, IGenericRepository<Staff> staff, IGenericRepository<Stock> stock, IGenericRepository<Order> orderRepo, IGenericRepository<OrderItem> orderItemRepo,
            IGenericRepository<Product> productRepo) { 
            _uow = uow; 
            _customerRepo = customer;
            _staffRepo = staff;
            _stockRepo = stock;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _productRepo = productRepo;

            
        }

        public ErrorOrderDto Add(OrderDto orderDto)
        {
            var staff = _staffRepo.GetById(orderDto.StaffId);
            var customer = _customerRepo.GetById(orderDto.CustomerId);

            var stocks = _stockRepo.Get(x => x.StoreId == staff.StoreId).ToList();
            var products = _productRepo.Get().ToList();
            
            if (customer == null)
            {
                throw new Exception("no customer by this ID");
                //create a new customer, would need to go back to api and request more info from user
            }


            //Load Order container with all info that you need

            //put all stocks in it for that store
            //put all products in it and staff and customer



            //var order = customer.CreateOrder(orderContainer); // hold info as well so can validate
            //if (!orderContainer.IsValid)
            //{
            //    return Dtos with errors
            //}

            //return Dtos from order;

            //put 100k orders into azure database 
            //some customers go to an actual store and get served by staff member but they are introducing a website where they can fill an iorder with stcok from any of the stores (not a store specific check)
            //Look up strategy pattern (for when theres multiple cases) - 
            //look up blocking queue 
            //look up changing the behaviour for different cases




            OrderContainer orderContainer = new OrderContainer(staff.StaffId, staff.StoreId, stocks, products);



            foreach (OrderProductDto orderProductDto in orderDto.Products.ToList())
            {               
                orderContainer.AddOrderItem(orderProductDto.ItemId, orderProductDto.ProductId, orderProductDto.Quantity, orderProductDto.Discount);
            }
            orderContainer.Validate();


            ErrorOrderDto errorDto = new ErrorOrderDto
            {
                StaffId = orderContainer.StaffId,
                CustomerId = customer.CustomerId
                //order identifier needed
               
            };
            if (!orderContainer.IsValid)
            {

                errorDto.ItemErrors = orderContainer.Errors.Select(kv => new ErrorOrderItemDto
                {
                    ProductId = kv.Key.ProductId,
                    Error = kv.Value
                }).ToList();
                
                return errorDto;
            }

            Order order = customer.CreateOrder(orderContainer);
            _orderRepo.Insert(order);

            try
            {
                

                _uow.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Stock stock)
                    {
                        var databaseValues = entry.GetDatabaseValues();

                        if (databaseValues != null)
                        {
                            
                            var databaseQuantity = (int)databaseValues[nameof(Stock.Quantity)];
                            stock.RollbackQuantity(databaseQuantity);
                           
                        }
                        
                    }
                }
            }



            return errorDto;

           
          
        }



    }
}