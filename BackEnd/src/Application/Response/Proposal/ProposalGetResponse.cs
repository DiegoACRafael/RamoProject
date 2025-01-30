using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Response.Product;

namespace Application.Response.Proposal
{
    public class ProposalGetResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProductGetResponse> Products { get; set; }

    }
}