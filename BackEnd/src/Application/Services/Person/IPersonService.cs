using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Person;
using Application.Response;
using Application.Response.Person;

namespace Application.Services
{
    public interface IPersonService
    {
        Task<PagedResponse<List<PersonGetAllResponse>>> GetAsync();
        Task<BaseResponse<CreatePersonResponse>> CreateAsync(CreatePersonRequest request);
        Task<BaseResponse<PersonGetByIdResponse>> GetByIdAsync(Guid id);
        Task<BaseResponse<PersonUpdateResponse>> UpdateAsync(Guid id, PersonUpdateRequest request);
        Task<BaseResponse<PersonDeleteResponse>> DeleteAsync(Guid id);
    }
}