using System;
using System.Threading.Tasks;
using Application.Request.Person;
using Application.Response;
using Application.Response.Person;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("v1/lists-persons")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<PersonGetAllResponse>))]
        public async Task<IActionResult> GetAsync()
        {
            var persons = await _personService.GetAsync();

            if (persons == null)
                return NotFound();

            return Ok(persons);
        }


        [HttpGet("v1/persons-by-id/{id:Guid}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<PersonGetByIdResponse>))]
        public async Task<IActionResult> GetByAsync([FromRoute] Guid id)
        {
            var person = await _personService.GetByIdAsync(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost("v1/created-persons")]
        [ProducesResponseType(201, Type = typeof(BaseResponse<CreatePersonResponse>))]
        public async Task<IResult> PostAsync([FromBody] CreatePersonRequest request)
        {
            var result = await _personService.CreateAsync(request);

            return result.IsSuccess
                 ? TypedResults.Created($"v1/person-by-id/{result.Data}", result)
                 : TypedResults.BadRequest(result.Data);
        }

        [HttpPut("v1/update-persons/{id:Guid}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<PersonUpdateResponse>))]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, PersonUpdateRequest request)
        {
            var person = await _personService.UpdateAsync(id, request);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpDelete("v1/remove-persons/{id:Guid}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<PersonDeleteResponse>))]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var person = await _personService.DeleteAsync(id);

            if (person is null)
            {
                return NoContent();
            }
            return Ok(person);

        }
    }
}