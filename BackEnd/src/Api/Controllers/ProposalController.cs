using System.Threading.Tasks;
using Application.Request.Proposal;
using Application.Response;
using Application.Response.Proposal;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProposalController : ControllerBase
    {
        private readonly IProposalService _proposalService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProposalController(IProposalService proposalService, UserManager<IdentityUser> userManager)
        {
            _proposalService = proposalService;
            _userManager = userManager;
        }

        [HttpPost("v1/created-proposal")]
        [ProducesResponseType(201, Type = typeof(BaseResponse<CreateProposalResponse>))]
        public async Task<IResult> PostAsync([FromBody] CreateProposalRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _proposalService.Create(request, user.Id);

            return result.IsSuccess
                 ? TypedResults.Created($"v1/Product-by-id/{result.Data}", result)
                 : TypedResults.BadRequest(result.Data);
        }
    }
}