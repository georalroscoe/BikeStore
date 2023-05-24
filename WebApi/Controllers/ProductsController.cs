using System;
using Microsoft.AspNetCore.Mvc;
using Application;
using Application.Interfaces;
using Dtos;
using DataAccess.Repositories;
using DataAccess;
using Domain;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ICreateProducts _createProduct;

        public ProductsController(ICreateProducts createProduct)
        {
            _createProduct = createProduct;
        }


        // POST api/<ProductBatcherController>
        //[HttpPost]
        //public void Post([FromBody] ProductDto dto)
        //{
        //    if (dto == null) {
        //        throw new ArgumentNullException(nameof(dto)); 
        //    }

        //    _createProduct.Add(dto);

    }











}

