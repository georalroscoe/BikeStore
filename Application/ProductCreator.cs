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
    public class ProductCreator : ICreateProducts
    {

        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Brand> _brandRepo;
        

        public ProductCreator(IUnitOfWork uow, IGenericRepository<Brand> brandRepo)
        {
            _uow = uow;
            _brandRepo = brandRepo;
        }

        public void Add(ProductDto productDto)
        {
            Brand brand = _brandRepo.GetById(productDto.BrandId);
            if (brand == null)
            {
                throw new Exception("Brand is null");
            }

            brand.AddProduct(productDto.ProductName, productDto.CategoryId, productDto.ModelYear, productDto.ListPrice);

            _uow.Save();
            //dto with brand id, catergory id, productName, model year, list price,
            //find the brand, create product in that brand and add to brand list (ask whether this is neccessary, imo don't think it is, info will be retrieved when neccessary)
            //do i add to list in categories?
            //could do checks for all the parameters 
            //doesnt seem like much logic involved here




            
        }



    }
}