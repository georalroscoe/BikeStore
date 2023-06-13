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

        public void SubstituteProducts(OrderContainer orderContainer, Customer customer, ErrorOrderDto crudDto)
        {

            
            if (!orderContainer.IsValid)
            {

                crudDto.ItemErrors = orderContainer.Errors.Select(kv => new ErrorOrderItemDto
                {
                    ProductId = kv.Key.ProductId,
                    Error = kv.Value
                }).ToList();

                
            }
            

        }
    }

        public class SubstitutionStrategy : ISubstituteStrategy
        {
        private readonly IGenericRepository<Order> _orderRepo;

        public SubstitutionStrategy(IGenericRepository<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }


        public void SubstituteProducts(OrderContainer orderContainer, Customer customer, ErrorOrderDto crudDto)
            {

                var productIds = orderContainer.Errors.Select(kv => kv.Key.ProductId).ToList();

                orderContainer.SubstituteProduct(productIds);

            
            Order? order = customer.CreateOrder(orderContainer);
            if (order == null)
            {
                NoSubstitutionStrategy noSubstitutionStrategy = new NoSubstitutionStrategy();
                noSubstitutionStrategy.SubstituteProducts(orderContainer, customer, crudDto);
                //call method here
                return;
                //call the other method and return dto, identify whats been substituted as well
            }
            else
            {
                _orderRepo.Insert(order);
            }

                


            }
        }


    
}
