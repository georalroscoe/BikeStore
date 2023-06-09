﻿using Application.Factories;
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
    public class StockStrategyFactory : IStockStrategyFactory
    {
        //private readonly DbContext _context;
        private readonly IGenericRepository<Stock> _stockRepo;

        public StockStrategyFactory(IGenericRepository<Stock> stockRepo) //DbContext context)
        {
            //_context = context;
            _stockRepo = stockRepo;
        }
        
            
        

        public IStockStrategy Create(bool useAllStoresStrategy)
        {
            //var serviceProvider = _context.GetService<IServiceProvider>();

            if (useAllStoresStrategy)
            {
                return new AllStoresStockStrategy(_stockRepo);
                //return serviceProvider.GetRequiredService<AllStoresStockStrategy>();
            }
            else
            {
                return new StoreStockStrategy(_stockRepo);
                //return serviceProvider.GetRequiredService<StoreStockStrategy>();
            }
        }
    }

}
