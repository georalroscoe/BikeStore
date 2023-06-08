using Application.Factories;
using Application.Interfaces;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class SubstitutionStrategyFactory : ISubstituteStrategyFactory
    {
        private readonly IGenericRepository<Order> _orderRepo;
        public SubstitutionStrategyFactory(IGenericRepository<Order> orderRepo) //DbContext context)
        {
            //_context = context;
            _orderRepo = orderRepo;
        }




        public ISubstituteStrategy Create(bool allowSubstitutions)
        {
            //var serviceProvider = _context.GetService<IServiceProvider>();

            if (allowSubstitutions)
            {
                return new SubstitutionStrategy(_orderRepo);
                //return serviceProvider.GetRequiredService<AllStoresStockStrategy>();
            }
            else
            {
                return new NoSubstitutionStrategy();
                //return serviceProvider.GetRequiredService<StoreStockStrategy>();
            }
        }
    }

}
