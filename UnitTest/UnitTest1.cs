using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using Domain;
using Application;
using Application.Interfaces;
using Microsoft.Identity.Client;
using Dtos;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.EntityFrameworkCore;



namespace UnitTest

{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FillDatabase()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BikeStoreContext>()
    .UseInMemoryDatabase(databaseName: "TestDatabase")
    .Options;

            using (var dbContext = new BikeStoreContext(dbContextOptions))
            {
                // Create an instance of the real UnitOfWork, passing the DbContext
                var unitOfWork = new UnitOfWork(dbContext);

                // Create an instance of your BrandSeeder class, passing the UnitOfWork and the real repository
                var brandSeeder = new BrandSeeder(unitOfWork, new GenericRepository<Brand>(dbContext));

                // Define the number of brands you want to seed
                int numberOfBrands = 5;

                // Call the SeedBrands method
                brandSeeder.SeedBrands(numberOfBrands);

                // Retrieve the brands added to the DbContext
                var addedBrands = dbContext.Brands.ToList();

                // Assert that the count of added brands is equal to the expected number
                Assert.AreEqual(numberOfBrands, addedBrands.Count);

                var categorySeeder = new CategorySeeder(unitOfWork, new GenericRepository<Category>(dbContext));

                int numberOfCategories = 5;

                categorySeeder.SeedCategories(numberOfCategories);

                var addedCategories = dbContext.Categories.ToList();

                Assert.AreEqual (numberOfCategories, addedCategories.Count);


                var productSeeder = new ProductSeeder(unitOfWork, new GenericRepository<Brand>(dbContext), new GenericRepository<Category>(dbContext), new GenericRepository<Product>(dbContext));

                int numberOfProducts = 5;

                productSeeder.SeedProducts(numberOfProducts);

                Assert.AreEqual(numberOfProducts, 5);
                

            }





            //        Random random = new Random();
            //        List<Brand> brandList = new List<Brand>();
            //        Brand cannondale = new Brand(random.Next(100_000_000, 1_000_000_000), "Cannondale");
            //        Brand trek = new Brand(random.Next(100_000_000, 1_000_000_000), "Trek");
            //        Brand canyon = new Brand(random.Next(100_000_000, 1_000_000_000), "Canyon");
            //        brandList.Add(cannondale);
            //        brandList.Add(trek);
            //        brandList.Add(canyon);

            //        new Category(random.Next(100_000_000, 1_000_000_000), "Mountain");
            //        new Category(random.Next(100_000_000, 1_000_000_000), "Road");
            //        new Category(random.Next(100_000_000, 1_000_000_000), "Electric");
            //        new Category(random.Next(100_000_000, 1_000_000_000), "Trail");
            //        new Category(random.Next(100_000_000, 1_000_000_000), "Kids");



            //        foreach (var brand in brandList) { 


            //            for (int j = 0; j < 5; j++)
            //            {
            //                string modelName = GenerateRandomString();
            //                int categoryNumber = random.Next(0, 5);
            //                short year = (short)random.Next(2018, 2024);
            //                decimal price = (decimal)random.Next(700, 5001) / 100;
            //                brand.AddProduct(random.Next(100_000_000, 1_000_000_000), modelName, categoryNumber, year, price);

            //            }

            //        }
            //        string GenerateRandomString()
            //        {
            //            string[] wordList = {
            //    "Topstone", "Superfly", "Strive", "Ripmo", "Rascal",
            //    "Jekyll", "Stache", "Levo", "Fuse", "Ripley",
            //    "Epic", "Enduro", "Spectral", "Trance", "Domane",
            //    "Marlin", "Slash", "Remedy", "Fuel", "Hardrock",
            //    "Blur", "Stumpjumper", "Talon", "X-Caliber", "Giant",
            //    "Rockhopper", "Anthem", "Roscoe", "Status", "Norco",
            //    "Process", "Fathom", "Pitch", "Yukon", "Pivot",
            //    "Tern", "Turbo", "Salsa", "Chameleon", "Warden"
            //};
            //            Random random = new Random();
            //            return wordList[random.Next(wordList.Length)];
            //        }

            //        List<Customer> customerList = new List<Customer>();
            //        for (int i = 1; i <= 10; i++)
            //        {
            //            string firstName = "Customer" + i;
            //            string lastName = "Lastname" + i;
            //            string phone = "Phone" + i;
            //            string email = "customer" + i + "@example.com";
            //            string street = "Street " + i;
            //            string city = "City " + i;
            //            string state = "State " + i;
            //            string zipCode = "Zip" + i;

            //            Customer customer = new Customer(random.Next(100_000_000, 1_000_000_000), firstName, lastName, phone, email, street, city, state, zipCode);
            //           customerList.Add(customer);
            //        }
            //        List<Store> storeList = new List<Store>();

            //        for (int i = 1; i <= 3; i++)
            //        {
            //            string storeName = "Store " + i;
            //            string phone = "Phone " + i;
            //            string email = "store" + i + "@example.com";
            //            string street = "Street " + i;
            //            string city = "City " + i;
            //            string state = "State " + i;
            //            string zipCode = "Zip " + i;

            //            Store store = new Store(random.Next(100_000_000, 1_000_000_000), storeName, phone, email, street, city, state, zipCode);
            //            storeList.Add(store);
            //        }
            //        List<Staff> staffList = new List<Staff>();
            //        for (int i = 1; i <= 20; i++)
            //        {
            //            string firstName = "Staff" + i;
            //            string lastName = "Lastname" + i;
            //            string email = "staff" + i + "@example.com";
            //            string phone = "Phone" + i;
            //            byte active = 1;
            //            int storeId = GetRandomStoreId();
            //            int? managerId = GetRandomManagerId(storeId);

            //            Store store = storeList.FirstOrDefault(x => x.StoreId== storeId);
            //            Staff staff = new Staff(random.Next(100_000_000, 1_000_000_000), firstName, lastName, email, phone, active, storeId, managerId);
            //            staffList.Add(staff);
            //            store.Staff.Add(staff);
            //        }

            //        int GetRandomStoreId()
            //        {
            //            Random random = new Random();
            //            var storeIds = storeList.Select(s => s.StoreId).ToList();
            //            return storeIds[random.Next(0, storeIds.Count)];
            //        }

            //        // Helper method to get a random manager ID from the same store
            //        int? GetRandomManagerId(int storeId)
            //        {
            //            Random random = new Random();
            //            var managers = staffList.Where(s => s.StoreId == storeId).ToList();
            //            if (managers.Count > 0)
            //                return managers[random.Next(0, managers.Count)].StaffId;
            //            else
            //                return null;
            //        }


            //        foreach (var store in storeList)
            //        {
            //            for (int i = 0; i < 10; i++)
            //            {
            //                int randomProductId = GetRandomProductId();
            //                int randomQuantity = random.Next(1, 51);

            //                AddStockToStore(store, randomProductId, randomQuantity);
            //            }
            //        }
            //        int GetRandomProductId()
            //        {
            //            Random random = new Random();
            //            var productIds = brandList.SelectMany(b => b.Products.Select(p => p.ProductId)).ToList();
            //            return productIds[random.Next(0, productIds.Count)];
            //        }

            //        void AddStockToStore(Store store, int productId, int quantity)
            //        {
            //            Stock stock = new Stock(store.StoreId, productId, quantity);
            //            store.AddStock(stock.ProductId, stock.Quantity);
            //        }
            //        //create a load of stock
            //        //
            //        int GetRandomStaffId(List<Staff> staffList)
            //        {
            //            Random random = new Random();
            //            var staffIds = staffList.Select(s => s.StaffId).ToList();
            //            return staffIds[random.Next(0, staffIds.Count)];
            //        }

            //        int GetRandomCustomerId(List<Customer> customerList)
            //        {
            //            Random random = new Random();
            //            var customerIds = customerList.Select(c => c.CustomerId).ToList();
            //            return customerIds[random.Next(0, customerIds.Count)];
            //        }

            //        List<OrderProductDto> orderProductList = new List<OrderProductDto>();


            //        for (int i = 0; i < 5; i++)
            //        {
            //            int itemId = random.Next(100_000_000, 1_000_000_000);
            //            int productId = GetRandomProductId(); // Implement this method as mentioned before
            //            decimal discount = GetRandomDiscount();
            //            int quantity = random.Next(1, 5);

            //            OrderProductDto orderProductDto = new OrderProductDto
            //            {
            //                ItemId = itemId,
            //                ProductId = productId,
            //                Discount = discount,
            //                Quantity = quantity
            //            };

            //            orderProductList.Add(orderProductDto);
            //        }

            //        decimal GetRandomDiscount()
            //        {
            //            int[] discounts = { 0, 20, 50 };
            //            return discounts[random.Next(discounts.Length)];
            //        }
            //        var orderDto = new OrderDto
            //        {
            //            StaffId = GetRandomStaffId(staffList),
            //            CustomerId = GetRandomCustomerId(customerList),
            //            Products = orderProductList

            //            // Set properties of the orderDto
            //        };

            //        Add_ValidOrderDto_CallsRepositoriesAndUnitOfWorkSave(orderDto);


        }

    }
}