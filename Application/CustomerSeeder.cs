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
            Random random = new Random();

            for (int i = 1; i <= numberOfCustomers; i++)
            {
                string firstName = "Customer" + i;
                string lastName = "Lastname" + i;
                string phone = random.Next(100_000_000, 1_000_000_000).ToString("D9");
                string email = "customer" + i + "@example.com";
                string street = "Street " + i;
                string city = "City " + i;
                string state = "State " + i;
                string zipCode = random.Next(1_000, 10_000).ToString("D5");

                Customer customer = new Customer(firstName, lastName, phone, email, street, city, state, zipCode);
                _customerRepo.Insert(customer);
            }

            _uow.Save();
        }




    }
}