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


        public ProductSeeder(IUnitOfWork uow, IGenericRepository<Brand> brandRepo)
        {
            _uow = uow;
            _brandRepo = brandRepo;
        }

        public Brand SeedProducts(int numberOfProducts)
        {
            List<Brand> brands = new List<Brand>();
            brands = _brandRepo.Get().ToList();
            return brands.FirstOrDefault();

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