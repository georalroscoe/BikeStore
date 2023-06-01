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
    public class BrandSeeder  : ISeedBrands
    {

        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Brand> _brandRepo;


        public BrandSeeder(IUnitOfWork uow, IGenericRepository<Brand> brandRepo)
        {
            _uow = uow;
            _brandRepo = brandRepo;
        }


        public void SeedBrands(int numberOfBrands)
        {
            for (int i = 0; i < numberOfBrands; i++)
            {
                
                string brandName = "Brand" + $"{i}"; // Replace this with your own logic to generate brand names
                Brand brand = new Brand(brandName);
                _brandRepo.Insert(brand);
                // Perform any additional operations with the created brand
                // For example, you could add it to a list, store it in a database, or perform other actions
                
            }
            _uow.Save();

        }



    }
}