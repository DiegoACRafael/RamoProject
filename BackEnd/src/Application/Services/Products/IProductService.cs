using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Request.Product;
using Application.Response;
using Application.Response.Product;

namespace Application.Services
{
    public interface IProductService
    {
        Task<PagedResponse<List<ProductGetAllResponse>>> GetAsync();
        Task<BaseResponse<ProductGetByIdResponse>> GetById(Guid id);
        Task<BaseResponse<CreateProductResponse>> Create(CreateProductRequest request);
        Task<BaseResponse<DeleteProductResponse>> DeleteAsync(Guid id);
        Task<BaseResponse<UpdateProductResponse>> UpdateAsync(Guid id, UpdateProductRequest request);
    }
}