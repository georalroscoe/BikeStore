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
    public class DatabaseFillerTests
    {
        private BikeStoreContext _dbContext;
        private IUnitOfWork _unitOfWork;
        private ISeedBrands _brandSeeder;
        private ISeedCategories _categorySeeder;
        private ISeedProducts _productSeeder;
        private ISeedCustomers _customerSeeder;
        private ISeedStores _storeSeeder;

        [TestInitialize]
        public void Initialize()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BikeStoreContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new BikeStoreContext(dbContextOptions);
            _unitOfWork = new UnitOfWork(_dbContext);
            _brandSeeder = new BrandSeeder(_unitOfWork, new GenericRepository<Brand>(_dbContext));
            _categorySeeder = new CategorySeeder(_unitOfWork, new GenericRepository<Category>(_dbContext));
            _productSeeder = new ProductSeeder(
                _unitOfWork,
                new GenericRepository<Brand>(_dbContext),
                new GenericRepository<Category>(_dbContext),
                new GenericRepository<Product>(_dbContext));
            _customerSeeder = new CustomerSeeder(_unitOfWork, new GenericRepository<Customer>(_dbContext));
            _storeSeeder = new StoreSeeder(_unitOfWork, new GenericRepository<Store>(_dbContext), new GenericRepository<Staff>(_dbContext), new GenericRepository<Product>(_dbContext), new GenericRepository<Stock>(_dbContext));
            
        }

        [TestMethod]
        public void FillDatabase()
        {
            // Define the number of brands you want to seed
            int numberOfBrands = 5;

            // Call the SeedBrands method
            _brandSeeder.SeedBrands(numberOfBrands);

            // Retrieve the brands added to the DbContext
            var addedBrands = _dbContext.Brands.ToList();

            // Assert that the count of added brands is equal to the expected number
            Assert.AreEqual(numberOfBrands, addedBrands.Count);

            // Define the number of categories you want to seed
            int numberOfCategories = 5;

            // Call the SeedCategories method
            _categorySeeder.SeedCategories(numberOfCategories);

            // Retrieve the categories added to the DbContext
            var addedCategories = _dbContext.Categories.ToList();

            // Assert that the count of added categories is equal to the expected number
            Assert.AreEqual(numberOfCategories, addedCategories.Count);

            // Define the number of products you want to seed
            int numberOfProducts = 5;

            // Call the SeedProducts method
            _productSeeder.SeedProducts(numberOfProducts);

            int numberOfCustomers = 40;

            _customerSeeder.SeedCustomers(numberOfCustomers);

            var addedCustomers = _dbContext.Customers.ToList();

            int numberOfStores = 10;

            _storeSeeder.SeedStores(numberOfStores);

            var addedStores = _dbContext.Stores.ToList();

            var addedStock = _dbContext.Stocks.ToList();

            int egg = 8;


            
        }
        [TestMethod]
        public void RetrieveCustomers()
        {
            // Retrieve the customers from the DbContext
            var customers = _dbContext.Customers.ToList();

            // Assert that the count of retrieved customers matches the number of seeded customers
            int expectedCustomerCount = 40;
            Assert.AreEqual(expectedCustomerCount, customers.Count);

            // Perform additional assertions or verifications on the retrieved customers if needed
            // ...
        }
        [TestMethod]
        public void TestOrder()
        {
            
       
            var customers = _dbContext.Customers.ToList();

            int expectedCustomerCount = 40;
            Assert.AreEqual(expectedCustomerCount, customers.Count);

            var addedCategories = _dbContext.Categories.ToList();

            var addedStaff = _dbContext.Staffs.ToList();
            // Arrange
            OrderDto orderDto = new OrderDto
            {
                StaffId = _dbContext.Staffs.FirstOrDefault().StaffId,
                CustomerId = _dbContext.Customers.FirstOrDefault().CustomerId,
                Products = new List<OrderProductDto>()
            };

            int itemId = 1; // Starting item ID

            for (int i = 0; i < 1; i++)
            {
                int productId = _dbContext.Products.Skip(i).First().ProductId; // Get product ID from seeded products
                decimal discount = i switch
                {
                    0 => 0,    // First order product: 0% discount
                    1 => 25,   // Second order product: 25% discount
                    2 => 50,   // Third order product: 50% discount
                    _ => 0     // Remaining order products: 0% discount
                };
                int quantity = new Random().Next(2, 6); // Generate a random quantity between 2 and 5

                orderDto.Products.Add(new OrderProductDto
                {
                    ItemId = itemId,
                    ProductId = productId,
                    Discount = discount,
                    Quantity = quantity
                });

                itemId++; // Increment item ID for the next order product
            }

            // Act
            OrderCreator orderCreator = new OrderCreator(_unitOfWork, new GenericRepository<Customer>(_dbContext), new GenericRepository<Staff>(_dbContext), new GenericRepository<Stock>(_dbContext));
            orderCreator.Add(orderDto);

        }



        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Dispose();
        }
    }




}
