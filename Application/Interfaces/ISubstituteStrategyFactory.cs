using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;

namespace Application.Factories
{
    public interface ISubstituteStrategyFactory
    {
        ISubstituteStrategy Create(bool useAllStoresStrategy);
    }
}

