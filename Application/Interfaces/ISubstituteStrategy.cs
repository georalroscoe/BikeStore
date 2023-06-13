using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos;

namespace Application.Interfaces
{
    public interface ISubstituteStrategy
    {
        void SubstituteProducts(OrderContainer orderContainer, Customer customer, ErrorOrderDto crudDto);
    }

}
