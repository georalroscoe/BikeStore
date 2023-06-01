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
    public class CustomerSeeder : ISeedCustomers
    {

        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Customer> _customerRepo;
       


        public CustomerSeeder(IUnitOfWork uow, IGenericRepository<Customer> customerRepo)
        {
            _uow = uow;
            _customerRepo = customerRepo;

           
        }

        public void SeedCustomers(int numberOfCustomers)
        {
            
            for (int i = 1; i <= numberOfCustomers; i++)
            {
                string firstName = "Customer" + i;
                string lastName = "Lastname" + i;
                string phone = "Phone" + i;
                string email = "customer" + i + "@example.com";
                string street = "Street " + i;
                string city = "City " + i;
                string state = "State " + i;
                string zipCode = "Zip" + i;

                Customer customer = new Customer(firstName, lastName, phone, email, street, city, state, zipCode);
                _customerRepo.Insert(customer);
            }
            _uow.Save();

        }



    }
}