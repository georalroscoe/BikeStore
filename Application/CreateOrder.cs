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


namespace Application { 
    public class CreateOrder : ICreateOrders
    {

        private readonly IUnitOfWork _uow;

        public CreateOrder(IUnitOfWork uow) { _uow = uow; }

        public void OrderCreator(OrderDto orderDto)
        {

            
            //productid, quantity, discount, itemid passed to a list on a dto with staff and customerid
            //set order date, shipping date 5 business days from that date, shipped date might have to be dealt with
            //get store from the staff
            
            //check stock at that store,
            //take away quantity from stock
            //check if there is a customer, if not make one

            //
        }



    }
}