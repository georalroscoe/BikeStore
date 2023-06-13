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
    public class StoreSeeder : ISeedStores
    {

        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Store> _storeRepo;
        private readonly IGenericRepository<Staff> _staffRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<Stock> _stockRepo;



        public StoreSeeder(IUnitOfWork uow, IGenericRepository<Store> storeRepo, IGenericRepository<Staff> staffRepo, IGenericRepository<Product> productRepo, IGenericRepository<Stock> stockRepo)
        {
            _uow = uow;
            _storeRepo = storeRepo;
            _staffRepo = staffRepo;
            _productRepo = productRepo;
            _stockRepo = stockRepo;
           


        }

        public void SeedStores(int numberOfStores)
        {
            Random random = new Random();
            List<Store> stores = new List<Store>();
            for (int i = 1; i <= numberOfStores; i++)
            {
                string storeName = "Store " + i;
                string phone = random.Next(100_000_000, 1_000_000_000).ToString("D9");
                string email = "store" + i + "@example.com";
                string street = "Street " + i;
                string city = "City " + i;
                string state = "State " + i;
                string zipCode = random.Next(1_000, 10_000).ToString("D5");

                Store store = new Store(storeName, phone, email, street, city, state, zipCode);
                _storeRepo.Insert(store);
                stores.Add(store);
            }

            int j = 0;
            
            foreach(Store store in stores)
            {

                for (int i = 0; i < random.Next(11, 50); i++)
                {
                    string firstName = "Staff" + j + "_" + i;
                    string lastName = "Lastname" + j + "_" + i;
                    string email = "staff" + j + "_" + i + "@example.com";
                    string phone = random.Next(100_000_000, 1_000_000_000).ToString("D9");
                    byte active = 1;

                    Staff staff = store.AddStaff(firstName, lastName, email, phone, active);
                    _staffRepo.Insert(staff);
                    
                  
                }

               

                for (int i = 608; i <= 3107; i++)
                {
                    int randomSkip = random.Next(1, 101);

                    
                    if (randomSkip <= 2)
                    {
                       
                      
                        continue;
                    }
                    
                    //int randomProductId = GetRandomProductId();
                    int randomQuantity = random.Next(1, 81);

                    Stock? stock = store.AddStock(i, randomQuantity);

                    if (stock != null)
                    {
                        _stockRepo.Insert(stock);
                    }

                    
                    
                }
                j++;

            }
            //int GetRandomProductId()
            //{
            //    var productIds = _productRepo.Get().Select(p => p.ProductId).ToList();
            //    Random random = new Random();
            //    int randomIndex = random.Next(0, productIds.Count);
            //    return productIds[randomIndex];
            //}



            _uow.Save();

        }



    }
}