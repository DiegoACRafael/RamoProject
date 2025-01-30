using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Proposal;
using Application.Response;
using Application.Response.Proposal;

namespace Application.Services
{
    public interface IProposalService
    {
        Task<BaseResponse<CreateProposalResponse>> Create(CreateProposalRequest request, string userId);
        Task <BaseResponse<IEnumerable<ProposalGetResponse>>> GetByUserIdAsync(string userId);
    }
}