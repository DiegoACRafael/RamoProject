using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Proposal;
using Application.Response;
using Application.Response.Proposal;
using Domain.Model;
using Infra.EF.Interfaces;

namespace Application.Services
{
    public class ProposalService : IProposalService
    {
        private readonly IProposalRepository _prposalRepository;

        public ProposalService(IProposalRepository prposalRepository)
        {
            _prposalRepository = prposalRepository;
        }

        public async Task<BaseResponse<CreateProposalResponse>> Create(CreateProposalRequest request, string userId)
        {
            var proposal = new Proposal
            {
                PersnId = request.PersonId,
                UserId = userId,
            };

            proposal.ProposalProducts = [.. request.ProductsId.Select(p => new ProposalProduct { ProductId = p, ProposalId = proposal.Id })];

            await _prposalRepository.Create(proposal);

            await _prposalRepository.Commit();

            return new BaseResponse<CreateProposalResponse>();
        }
    }
}