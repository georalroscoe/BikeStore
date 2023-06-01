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
    public class CategorySeeder : ISeedCategories
    {

        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Category> _categoryRepo;


        public CategorySeeder(IUnitOfWork uow, IGenericRepository<Category> categoryRepo)
        {
            _uow = uow;
            _categoryRepo = categoryRepo;
           
        }

        public void SeedCategories(int numberOfCategories)
        {
            for (int i = 0; i < numberOfCategories; i++)
            {
                
                string categoryName = "Category" + $"{i}"; // Replace this with your own logic to generate category names
                Category category = new Category(categoryName);
                _categoryRepo.Insert(category);
            }
            _uow.Save();

        }



    }
}