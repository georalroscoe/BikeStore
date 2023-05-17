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

using System.Xml;


namespace Application { 
    public class CreateOrder : ICreateOrders
    {

        private readonly IUnitOfWork _uow;

        public CreateOrder(IUnitOfWork uow) { _uow = uow; }
    }
}