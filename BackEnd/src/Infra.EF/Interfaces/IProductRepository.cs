using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;

namespace Infra.EF.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {

    }
}