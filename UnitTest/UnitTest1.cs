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
using Microsoft.Extensions.Configuration;

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
        private ICreateOrders _orderCreator;
        private ICreateProducts _productCreator;

        [TestInitialize]
        public void Initialize()
        {
            var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var dbContextOptions = new DbContextOptionsBuilder<BikeStoreContext>()
                .UseSqlServer(connectionString)
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
            _orderCreator = new OrderCreator(_unitOfWork, new GenericRepository<Customer>(_dbContext), new GenericRepository<Staff>(_dbContext), new GenericRepository<Stock>(_dbContext), new GenericRepository<Order>(_dbContext), new GenericRepository<OrderItem>(_dbContext), new GenericRepository<Product>(_dbContext));
            _productCreator = new ProductCreator(_unitOfWork, new GenericRepository<Brand>(_dbContext), new GenericRepository<Product>(_dbContext));

        }

        [TestMethod]
        public void aFillDatabase()
        {

            //int numberOfBrands = 5;

            //_brandSeeder.SeedBrands(numberOfBrands);


            //var addedBrands = _dbContext.Brands.ToList();


            //Assert.AreEqual(numberOfBrands, addedBrands.Count);


            //int numberOfCategories = 5;


            //_categorySeeder.SeedCategories(numberOfCategories);


            //var addedCategories = _dbContext.Categories.ToList();


            ////Assert.AreEqual(numberOfCategories, addedCategories.Count);


            //int numberOfProducts = 100;


            //_productSeeder.SeedProducts(numberOfProducts);

            //int numberOfCustomers = 400;

            //_customerSeeder.SeedCustomers(numberOfCustomers);

            //var addedCustomers = _dbContext.Customers.ToList();

            //int numberOfStores = 30;

            //_storeSeeder.SeedStores(numberOfStores);



        }
        //[TestMethod]
        //public void ClearCustomerTable()
        //{
        //    var customers = _dbContext.Customers.ToList();
        //    _dbContext.Customers.RemoveRange(customers);
        //    _dbContext.SaveChanges();
        //}
        [TestMethod]
        public void cTestOrder()
        {
            Staff staff = _dbContext.Staffs.FirstOrDefault();

            OrderDto orderDto = new OrderDto
            {
                StaffId = staff.StaffId,
                CustomerId = _dbContext.Customers.FirstOrDefault().CustomerId,
                Products = new List<OrderProductDto>()
            };

            int itemId = 1;

            for (int i = 50; i < 52; i++)
            {
                int productId = _dbContext.Products.Skip(i).First().ProductId;
                decimal discount = i switch
                {
                    0 => 0,
                    1 => 25,
                    2 => 50,
                    _ => 0
                };
                int quantity = new Random().Next(2, 5);

                orderDto.Products.Add(new OrderProductDto
                {
                    ItemId = itemId,
                    ProductId = productId,
                    Discount = discount,
                    Quantity = quantity
                });

                itemId++;
            }


            //var initialStockQuantities = new Dictionary<int, int>();
            //foreach (var product in orderDto.Products)
            //{


            //    var stock = _dbContext.Stocks.FirstOrDefault(s => s.ProductId == product.ProductId && s.StoreId == staff.StoreId);
            //    if (stock == null)
            //    {

            //        continue;
            //    }

            //    initialStockQuantities[product.ProductId] = stock.Quantity;
            //}

            _orderCreator.Add(orderDto);


            //var createdOrder = _dbContext.Orders.FirstOrDefault();
            //Assert.IsNotNull(createdOrder);
            //Assert.AreEqual(orderDto.StaffId, createdOrder.StaffId);
            //Assert.AreEqual(orderDto.CustomerId, createdOrder.CustomerId);

            //int expectedTransactions = orderDto.Products.Count;
            //foreach (var product in orderDto.Products)
            //{
            //    var stock = _dbContext.Stocks.FirstOrDefault(s => s.ProductId == product.ProductId && s.StoreId == staff.StoreId);
            //    if (stock != null)
            //    {
            //        var initialQuantity = initialStockQuantities[product.ProductId];
            //        var expectedQuantity = initialQuantity - product.Quantity;
            //        Assert.AreEqual(expectedQuantity, stock.Quantity);
            //        expectedTransactions--;
            //    }
            //}

            //Assert.AreEqual(expectedTransactions, completedTransactions);


        }






        //[TestMethod]
        //    public void dAddProductTest()
        //    {



        //        var productDto = new ProductDto
        //        {
        //            ProductName = "Product",
        //            BrandId = 2,
        //            CategoryId = 1,
        //            ModelYear = 2023,
        //            ListPrice = 9.99m
        //        };

        //        _productCreator.Add(productDto);


        //        var addedProduct = _dbContext.Products.FirstOrDefault(x => x.ProductName == productDto.ProductName);
        //        Assert.IsNotNull(addedProduct);
        //        Assert.AreEqual(productDto.ProductName, addedProduct.ProductName);
        //        Assert.AreEqual(productDto.BrandId, addedProduct.BrandId);
        //        Assert.AreEqual(productDto.CategoryId, addedProduct.CategoryId);
        //        Assert.AreEqual(productDto.ModelYear, addedProduct.ModelYear);
        //        Assert.AreEqual(productDto.ListPrice, addedProduct.ListPrice);

        //    }






        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Dispose();
        }
    }




}
