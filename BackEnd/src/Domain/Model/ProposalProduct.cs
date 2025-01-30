using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Base;

namespace Domain.Model
{
    public class ProposalProduct : Entity
    {
        public Guid ProposalId { get; set; }
        public Guid ProductId { get; set; }

        public virtual Proposal Proposal { get; set; }
        public virtual Product Product { get; set; }
    }
}