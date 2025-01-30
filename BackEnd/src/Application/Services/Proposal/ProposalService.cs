using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Application.Request.Proposal;
using Application.Response;
using Application.Response.Product;
using Application.Response.Proposal;
using Domain.Model;
using Infra.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<BaseResponse<IEnumerable<ProposalGetResponse>>> GetByUserIdAsync(string userId)
        {
            var proposals = await _prposalRepository.GetAsync(x => x.UserId == userId,
                                                        include: p => p.Include(p => p.ProposalProducts)
                                                            .ThenInclude(p => p.Product)
                                                            .Include(p => p.Person));

            var response = proposals.Select(x => new ProposalGetResponse
            {
                Id = x.Id,
                Name = x.Person.Name,
                Products = x.ProposalProducts?.Select(x => x.Product)?.Select(p => new ProductGetResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                }).ToList()
            }).ToList();


            return (response is null)
                  ? new BaseResponse<IEnumerable<ProposalGetResponse>>(null, 404, "[FX042] Proposal does not exist")
                  : new BaseResponse<IEnumerable<ProposalGetResponse>>(response, message: "Successfully located");
        }
    }
}