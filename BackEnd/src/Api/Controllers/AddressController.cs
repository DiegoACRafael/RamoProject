using System;
using System.Threading.Tasks;
using Application.Request.Address;
using Application.Request.Person;
using Application.Response;
using Application.Response.Address;
using Application.Response.Person;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("v1/lists-addresss")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<AddressGetAllResponse>))]
        public async Task<IActionResult> GetAsync()
        {
            var Addresss = await _addressService.GetAsync();

            if (Addresss == null)
                return NotFound();

            return Ok(Addresss);
        }


        [HttpGet("v1/addresss-by-id/{id:Guid}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<AddressGetByIdResponse>))]
        public async Task<IActionResult> GetByAsync([FromRoute] Guid id)
        {
            var Address = await _addressService.GetByIdAsync(id);

            if (Address == null)
                return NotFound();

            return Ok(Address);
        }

        [HttpPost("v1/created-address")]
        [ProducesResponseType(201, Type = typeof(BaseResponse<CreateAddressResponse>))]
        public async Task<IResult> PostAsync([FromBody] CreateAddressRequest request)
        {
            var result = await _addressService.CreateAsync(request);

            return result.IsSuccess
                 ? TypedResults.Created($"v1/address-by-id/{result.Data}", result)
                 : TypedResults.BadRequest(result.Data);
        }

        [HttpPut("v1/update-addresss/{id:Guid}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<AddressUpdateResponse>))]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, AddressUpdateRequest request)
        {
            var Address = await _addressService.UpdateAsync(id, request);

            if (Address == null)
                return NotFound();

            return Ok(Address);
        }

        [HttpDelete("v1/remove-addresss/{id:Guid}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<AddressDeleteResponse>))]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var Address = await _addressService.DeleteAsync(id);

            if (Address is null)
            {
                return NoContent();
            }
            return Ok(Address);

        }
    }
}