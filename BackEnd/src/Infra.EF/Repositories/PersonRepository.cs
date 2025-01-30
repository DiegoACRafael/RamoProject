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
    public class PersonRepository : EfRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDataContext context) 
        : base(context)
        {
        }
    }
}