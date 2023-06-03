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


        public OrderCreator (IUnitOfWork uow, IGenericRepository<Customer> customer, IGenericRepository<Staff> staff, IGenericRepository<Stock> stock, IGenericRepository<Order> orderRepo, IGenericRepository<OrderItem> orderItemRepo ) { 
            _uow = uow; 
            _customerRepo = customer;
            _staffRepo = staff;
            _stockRepo = stock;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            
        }

        public void Add(OrderDto orderDto)
        {
            var staff = _staffRepo.GetById(orderDto.StaffId);
            var customer = _customerRepo.GetById(orderDto.CustomerId);

            var stocks = _stockRepo.Get().ToList();
            
            if (customer == null)
            {
                throw new Exception("no customer by this ID");
                //create a new customer, would need to go back to api and request more info from user
            }

            Validation validation = new Validation(staff.StoreId);
            Order newOrder = customer.AddOrder(staff.StoreId, staff.StaffId);
            _orderRepo.Insert(newOrder);
            

            foreach (OrderProductDto orderProductDto in orderDto.Products.ToList())
            {
                Stock? stock = _stockRepo.Get(x => x.StoreId == staff.StoreId && x.ProductId == orderProductDto.ProductId).FirstOrDefault();
                if (stock == null)
                {
                    continue;
                    throw new Exception("Product does not exist in stock list");
                }
                decimal listPrice = stock.Product.ListPrice;
                validation.AddOrderItem(stock, orderProductDto.ItemId, orderProductDto.ProductId, orderProductDto.Quantity, orderProductDto.Discount, listPrice);
               


            }

            


            validation.isValid();

            if (!validation.IsValid)
            {
                throw new Exception("return the error list in the vailidation domain");
            }




            foreach (OrderProductDto orderProductDto in orderDto.Products.ToList())
            {
                Stock? stock = _stockRepo.Get(x => x.StoreId == staff.StoreId && x.ProductId == orderProductDto.ProductId).FirstOrDefault();
                if (stock == null)
                {
                    continue;
                    throw new Exception("Product does not exist in stock list");
                }
                decimal listPrice = stock.Product.ListPrice;
                var currentTimeStamp = stock.TimeStamp;
                

                newOrder.FillOrder(currentTimeStamp, stock, orderProductDto.ItemId, orderProductDto.ProductId, orderProductDto.Quantity, orderProductDto.Discount, listPrice);


            }




            _uow.Save();
          
        }



    }
}