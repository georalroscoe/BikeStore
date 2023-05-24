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
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly ICreateOrders _createOrder;

        public OrdersController(ICreateOrders createOrder)
        {
            _createOrder = createOrder;
        }


        // POST api/<ProductBatcherController>
        [HttpPost]
        public void CreateOrder([FromBody] OrderDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            _createOrder.Add(dto);

        }











    }
}
