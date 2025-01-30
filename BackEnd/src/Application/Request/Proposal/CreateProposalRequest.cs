using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Request.Proposal
{
    public class CreateProposalRequest
    {
        public Guid PersonId { get; set; }
        public List<Guid> ProductsId { get; set; }
    }
}