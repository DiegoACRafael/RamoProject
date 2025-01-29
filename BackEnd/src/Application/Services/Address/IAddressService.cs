using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Address;
using Application.Response;
using Application.Response.Address;

namespace Application.Services
{
    public interface IAddressService
    {
        Task<PagedResponse<List<AddressGetAllResponse>>> GetAsync();
        Task<BaseResponse<AddressGetByIdResponse>> GetByIdAsync(Guid id);
        Task<BaseResponse<AddressUpdateResponse>> UpdateAsync(Guid id, AddressUpdateRequest request);
        Task<BaseResponse<AddressDeleteResponse>> DeleteAsync(Guid id);
    }
}