using Application.Interfaces;
using DataAccess.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class StoreStockStrategy : IStockStrategy
    {
        private readonly IGenericRepository<Stock> _stockRepo;

        public StoreStockStrategy(IGenericRepository<Stock> stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public List<Stock> GetStocks(int storeId)
        {
            return _stockRepo.Get(x => x.StoreId == storeId).ToList();
        }
    }
    public class AllStoresStockStrategy : IStockStrategy
    {
        private readonly IGenericRepository<Stock> _stockRepo;

        public AllStoresStockStrategy(IGenericRepository<Stock> stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public List<Stock> GetStocks(int storeId)
        {
            return _stockRepo.Get().ToList();
        }
    }


}
