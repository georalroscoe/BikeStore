﻿using Microsoft.EntityFrameworkCore.Storage;
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
            for (int i = 6; i < numberOfBrands; i++)
            {
                
                string brandName = "Brand" + $"{i}"; 
                Brand brand = new Brand(brandName);
                _brandRepo.Insert(brand);
                
                
            }
            _uow.Save();

        }



    }
}