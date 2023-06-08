using Application.Interfaces;
using DataAccess.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos;

namespace Application
{
    public class NoSubstitutionStrategy : ISubstituteStrategy
    {
        //{
        //    private readonly IGenericRepository<Stock> _stockRepo;
        //    private readonly IGenericRepository

        //    public NoSubstitutionStrategy(IGenericRepository<Stock> stockRepo)
        //    {
        //        _stockRepo = stockRepo;
        //    }

        public ErrorOrderDto SubstituteProducts(OrderContainer orderContainer, Customer customer)
        {

            ErrorOrderDto crudDto = new ErrorOrderDto
            {
                StaffId = orderContainer.StaffId,
                CustomerId = orderContainer.StoreId
                //order identifier needed

            };
            if (!orderContainer.IsValid)
            {

                crudDto.ItemErrors = orderContainer.Errors.Select(kv => new ErrorOrderItemDto
                {
                    ProductId = kv.Key.ProductId,
                    Error = kv.Value
                }).ToList();

                return crudDto;
            }
            return null;

        }
    }

        public class SubstitutionStrategy : ISubstituteStrategy
        {
        private readonly IGenericRepository<Order> _orderRepo;

        public SubstitutionStrategy(IGenericRepository<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }


        public ErrorOrderDto SubstituteProducts(OrderContainer orderContainer, Customer customer)
            {

                var productIds = orderContainer.Errors.Select(kv => kv.Key.ProductId).ToList();

                orderContainer.SubstituteProduct(productIds);

            
            Order? order = customer.CreateOrder(orderContainer);
            if (order == null)
            {
                throw new Exception("problem with code or no quantity anywhere");
                //call the other method and return dto, identify whats been substituted as well
            }
            else
            {
                _orderRepo.Insert(order);
            }

                return null;


            }
        }


    
}
