using Application.Factories;
using Application.Interfaces;
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
    public class StockStrategyFactory : IStockStrategyFactory
    {
        private readonly DbContext _context;

        public StockStrategyFactory(DbContext context)
        {
            _context = context;
        }
        
            
        

        public IStockStrategy Create(bool useAllStoresStrategy)
        {
            var serviceProvider = _context.GetService<IServiceProvider>();

            if (useAllStoresStrategy)
            {
                return serviceProvider.GetRequiredService<AllStoresStockStrategy>();
            }
            else
            {
                return serviceProvider.GetRequiredService<StoreStockStrategy>();
            }
        }
    }

}
