using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Infra.EF.Data.Context;
using Infra.EF.Interfaces;

namespace Infra.EF.Repositories
{
    public class ProposalRepository : EfRepository<Proposal>, IProposalRepository
    {
        public ProposalRepository(AppDataContext context) 
        : base(context)
        {
        }
    }
}