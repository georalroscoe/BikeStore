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
using Application.Factories;

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
        private readonly IStockStrategyFactory _stockStrategyFactory;
        private readonly ISubstituteStrategyFactory _substituteStrategyFactory;



        public OrderCreator(IUnitOfWork uow, IGenericRepository<Customer> customer, IGenericRepository<Staff> staff,
            IGenericRepository<Stock> stock, IGenericRepository<Order> orderRepo, IGenericRepository<OrderItem> orderItemRepo,
            IGenericRepository<Product> productRepo, IStockStrategyFactory stockStrategyFactory, ISubstituteStrategyFactory substituteStrategyFactory)
        {
            _uow = uow;
            _customerRepo = customer;
            _staffRepo = staff;
            _stockRepo = stock;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _productRepo = productRepo;
            _stockStrategyFactory = stockStrategyFactory;
            _substituteStrategyFactory = substituteStrategyFactory;
        }

        public ErrorOrderDto Add(OrderDto orderDto)
        {


            //by state that customer is in
            //find store with most of that in, or at least the best one
            //find a substition product with similar features
            var staff = _staffRepo.GetById(orderDto.StaffId);
            var customer = _customerRepo.GetById(orderDto.CustomerId);
            bool allStoresStrategy = orderDto.AllStoresStrategy;
            IStockStrategy stockStrategy = _stockStrategyFactory.Create(allStoresStrategy);
            


            var stocks = stockStrategy.GetStocks(staff.StoreId);

            var products = _productRepo.Get().ToList();
            
            if (customer == null)
            {
                throw new Exception("no customer by this ID");
                //create a new customer, would need to go back to api and request more info from user
            }


            OrderContainer orderContainer = new OrderContainer(staff.StaffId, staff.StoreId, stocks, products);



            foreach (OrderProductDto orderProductDto in orderDto.Products.ToList())
            {               
                orderContainer.AddOrderItem(orderProductDto.ItemId, orderProductDto.ProductId, orderProductDto.Quantity, orderProductDto.Discount);
            }


            ErrorOrderDto crudDto = new ErrorOrderDto
            {
                StaffId = orderContainer.StaffId,
                CustomerId = orderContainer.StoreId
                //order identifier needed

            };
            Order? order = customer.CreateOrder(orderContainer);


            if (order == null)
            {
                ISubstituteStrategy substituteStrategy = _substituteStrategyFactory.Create(true);
                substituteStrategy.SubstituteProducts(orderContainer, customer, crudDto);
            }

            else
            {
                _orderRepo.Insert(order);
            }
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



            return crudDto;

           
          
        }



    }
}