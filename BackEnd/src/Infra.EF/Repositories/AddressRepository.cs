using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Infra.EF.Data.Context;
using Infra.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.EF.Repositories
{
    public class AddressRepository : EfRepository<Address>, IAddressRepository
    {
        public AddressRepository(AppDataContext context) 
            : base(context)
        {
        }
    }
}