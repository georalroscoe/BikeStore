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
using Application.Factories;

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
        private IStockStrategyFactory _stockStrategyFactory;
        private ISubstituteStrategyFactory _substitutionStrategyFactory;

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
            _stockStrategyFactory = new StockStrategyFactory(new GenericRepository<Stock>(_dbContext));
            _substitutionStrategyFactory = new SubstitutionStrategyFactory(new GenericRepository<Order>(_dbContext));


            _brandSeeder = new BrandSeeder(_unitOfWork, new GenericRepository<Brand>(_dbContext));
            _categorySeeder = new CategorySeeder(_unitOfWork, new GenericRepository<Category>(_dbContext));
            _productSeeder = new ProductSeeder(
                _unitOfWork,
                new GenericRepository<Brand>(_dbContext),
                new GenericRepository<Category>(_dbContext),
                new GenericRepository<Product>(_dbContext));
            _customerSeeder = new CustomerSeeder(_unitOfWork, new GenericRepository<Customer>(_dbContext));
            _storeSeeder = new StoreSeeder(_unitOfWork, new GenericRepository<Store>(_dbContext), new GenericRepository<Staff>(_dbContext), new GenericRepository<Product>(_dbContext), new GenericRepository<Stock>(_dbContext));
            _orderCreator = new OrderCreator(_unitOfWork, new GenericRepository<Customer>(_dbContext), new GenericRepository<Staff>(_dbContext), new GenericRepository<Stock>(_dbContext), new GenericRepository<Order>(_dbContext), new GenericRepository<OrderItem>(_dbContext), new GenericRepository<Product>(_dbContext), _stockStrategyFactory, _substitutionStrategyFactory);
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

        //[TestMethod]
        //public void ClearOrderAndOrderItems()
        //{
        //    var orders = _dbContext.Orders.ToList();
        //    _dbContext.Orders.RemoveRange(orders);

        //    var orderItems = _dbContext.OrderItems.ToList();
        //    _dbContext.OrderItems.RemoveRange(orderItems);

        //    _dbContext.SaveChanges();
        //}

        [TestMethod]
        public void cTestOrder()
        {

            int GetRandomStaffId()
            {
                int totalStaffCount = _dbContext.Staffs.Count();
                int randomIndex = new Random().Next(totalStaffCount);

                return _dbContext.Staffs.Skip(randomIndex).Select(s => s.StaffId).FirstOrDefault();
            }

            int GetRandomCustomerId()
            {
                int totalCustomerCount = _dbContext.Customers.Count();
                int randomIndex = new Random().Next(totalCustomerCount);

                return _dbContext.Customers.Skip(randomIndex).Select(c => c.CustomerId).FirstOrDefault();
            }

            int GetRandomProductId()
            {
                int totalProductCount = _dbContext.Products.Count();
                int randomIndex = new Random().Next(totalProductCount);

                return _dbContext.Products.Skip(randomIndex).Select(p => p.ProductId).FirstOrDefault();
            }

            decimal GetRandomDiscount()
            {
                int[] availableDiscounts = { 0, 10, 20, 30, 40, 50 };
                int randomIndex = new Random().Next(availableDiscounts.Length);

                return availableDiscounts[randomIndex];
            }

            int[] GetShuffledProductIds()
            {
                var productIds = _dbContext.Products.Select(p => p.ProductId).ToArray();
                int n = productIds.Length;

                
                for (int i = n - 1; i > 0; i--)
                {
                    int j = new Random().Next(i + 1);
                    int temp = productIds[i];
                    productIds[i] = productIds[j];
                    productIds[j] = temp;
                }

                return productIds;
            }

            int numberOfOrders = 50;
            int numberOfProducts = 50;

            List<ErrorOrderDto> errorList = new List<ErrorOrderDto>();

            for (int orderCount = 0; orderCount < numberOfOrders; orderCount++)
            {
                HashSet<int> usedProductIds = new HashSet<int>();

                OrderDto orderDto = new OrderDto
                {
                    StaffId = GetRandomStaffId(),
                    CustomerId = GetRandomCustomerId(),
                    AllStoresStrategy = false,
                    Products = new List<OrderProductDto>()
                };

                for (int productCount = 0; productCount < numberOfProducts; productCount++)
                {
                    int productId;
                    do
                    {
                        productId = GetRandomProductId();
                    } while (usedProductIds.Contains(productId));

                    usedProductIds.Add(productId);

                    decimal discount = GetRandomDiscount();
                    int quantity = new Random().Next(30, 40);

                    orderDto.Products.Add(new OrderProductDto
                    {
                        ItemId = productCount + 1,
                        ProductId = productId,
                        Discount = discount,
                        Quantity = quantity
                    });
                }

                var errorDto = (_orderCreator.Add(orderDto));
                //if (errorDto.ItemErrors.Count > 0)
                //{
                //    errorList.Add(errorDto);
                //}
            }







            var sr = 5;


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



            //}

        
            [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Dispose();
        }
    }




}
