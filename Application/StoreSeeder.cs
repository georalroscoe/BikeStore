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
            List<Store> stores = new List<Store>();
            for (int i = 1; i <= numberOfStores; i++)
            {
                string storeName = "Store " + i;
                string phone = "Phone " + i;
                string email = "store" + i + "@example.com";
                string street = "Street " + i;
                string city = "City " + i;
                string state = "State " + i;
                string zipCode = "Zip " + i;

                Store store = new Store(storeName, phone, email, street, city, state, zipCode);
                _storeRepo.Insert(store);
                stores.Add(store);
            }

            Random random = new Random();
            //add staff
            foreach(Store store in stores)
            {
                for (int i = 0; i < random.Next(11, 50); i++)
                {
                    string firstName = "Staff" + i;
                    string lastName = "Lastname" + i;
                    string email = "staff" + i + "@example.com";
                    string phone = "Phone" + i;
                    byte active = 1;

                    Staff staff = store.AddStaff(firstName, lastName, email, phone, active);
                    _staffRepo.Insert(staff);
                    
                  
                }
                for (int i = 0; i < random.Next(5, 20); i++)
                {
                    int randomProductId = GetRandomProductId();

                    int randomQuantity = random.Next(1, 51);

                    Stock? stock = store.AddStock(randomProductId, randomQuantity);

                    if (stock != null)
                    {
                        _stockRepo.Insert(stock);
                    }
                    
                }
            }
            int GetRandomProductId()
            {
                var productIds = _productRepo.Get().Select(p => p.ProductId).ToList();
                Random random = new Random();
                int randomIndex = random.Next(0, productIds.Count);
                return productIds[randomIndex];
            }



            _uow.Save();

        }



    }
}