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
    public class ProductSeeder 
    {

        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Brand> _brandRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<Product> _productRepo;


        public ProductSeeder(IUnitOfWork uow, IGenericRepository<Brand> brandRepo, IGenericRepository<Category> categoryRepo, IGenericRepository<Product> productRepo)
        {
            _uow = uow;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        public void SeedProducts(int numberOfProducts)
        {
            List<Brand> brands = new List<Brand>();
            brands = _brandRepo.Get().ToList();
            Random random = new Random();

            int numberOfCategories = _categoryRepo.Get().Count();



            foreach (var brand in brands)
            {


                for (int j = 0; j < numberOfProducts; j++)
                {
                    string modelName = GenerateRandomString();
                    int categoryNumber = random.Next(0, numberOfCategories + 1);
                    short year = (short)random.Next(2018, 2024);
                    decimal price = (decimal)random.Next(700, 5001) / 100;
                    _productRepo.Insert(brand.AddProduct(modelName, categoryNumber, year, price));

                }

            }

            string GenerateRandomString()
            {
                string[] wordList = {
                "Topstone", "Superfly", "Strive", "Ripmo", "Rascal",
                "Jekyll", "Stache", "Levo", "Fuse", "Ripley",
                "Epic", "Enduro", "Spectral", "Trance", "Domane",
                "Marlin", "Slash", "Remedy", "Fuel", "Hardrock",
                "Blur", "Stumpjumper", "Talon", "X-Caliber", "Giant",
                "Rockhopper", "Anthem", "Roscoe", "Status", "Norco",
                "Process", "Fathom", "Pitch", "Yukon", "Pivot",
                "Tern", "Turbo", "Salsa", "Chameleon", "Warden"
            };
                Random random = new Random();
                return wordList[random.Next(wordList.Length)];
            }

            //for (int i = 0; i < numberOfCategories; i++)
            //{
            //    int categoryId = i + 1;
            //    string categoryName = "Category" + $"{i}"; // Replace this with your own logic to generate category names
            //    Category category = new Category(categoryId, categoryName);
            //    // Perform any additional operations with the created category
            //    // For example, you could add it to a list, store it in a database, or perform other actions
            //    Console.WriteLine($"Created category: Category ID - {category.CategoryId}, Category Name - {category.CategoryName}");
            //}
            //_uow.Save();

        }



    }
}