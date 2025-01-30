using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class Proposal : Entity
    {
        public Guid PersnId { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<ProposalProduct> ProposalProducts { get; set; }
        public virtual Person Person { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}