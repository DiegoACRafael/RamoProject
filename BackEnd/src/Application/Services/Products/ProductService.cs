using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Product;
using Application.Response;
using Application.Response.Product;
using Domain.Model;
using Infra.EF.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResponse<List<ProductGetResponse>>> GetAsync(int pageSize,int page)
        {
            var request = new ProductGetAllRequest{PageSize = pageSize, PageNumber = page};

            var products = await _productRepository.GetAll();

            var response = products.Select(a => new ProductGetResponse
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Price = a.Price,
            }).ToList();

            var paged = response.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            return (products is null)
                ? new PagedResponse<List<ProductGetResponse>>(null, 500, "[FX052] There is no registered products")
                : new PagedResponse<List<ProductGetResponse>>(paged, products.Count, request.PageNumber, request.PageSize, message: "List Products");
        }

        public async Task<BaseResponse<ProductGetByIdResponse>> GetById(Guid id)
        {
            var product = await _productRepository.GetById(id);

            var response = new ProductGetByIdResponse
            {

                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };

            return (product is null)
              ? new BaseResponse<ProductGetByIdResponse>(null, 404, "[FX042] Product does not exist")
              : new BaseResponse<ProductGetByIdResponse>(response, message: "Successfully located");
        }

        public async Task<BaseResponse<CreateProductResponse>> Create(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description

            };
            var productAdd = await _productRepository.Create(product);
            await _productRepository.Commit();

            var response = new CreateProductResponse
            {
                Id = productAdd.Id,
                Name = productAdd.Name,
                Description = productAdd.Description,
                Price = productAdd.Price,
            };

            return response is null
           ? new BaseResponse<CreateProductResponse>(null, 500, message: "[FX032] Failure to create Product")
           : new BaseResponse<CreateProductResponse>(response, message: "Successfully created Product");
        }

        public async Task<BaseResponse<UpdateProductResponse>> UpdateAsync(Guid id, UpdateProductRequest request)
        {
            var product = await _productRepository.GetById(id);

            if (product is null)
                return new BaseResponse<UpdateProductResponse>(null, 404, "[FX012] Product does not exist");

            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;

            var productUp = await _productRepository.Update(product);
            await _productRepository.Commit();

            var response = new UpdateProductResponse
            {
                Name = productUp.Name,
                Description = productUp.Description,
                Price = productUp.Price,

            };

            return (productUp is null)
                ? new BaseResponse<UpdateProductResponse>(null, 500, "[FX022] Failure to update product")
                : new BaseResponse<UpdateProductResponse>(response, message: "Product successfully updated");
        }

        public async Task<BaseResponse<DeleteProductResponse>> DeleteAsync(Guid id)
        {
            var product = await _productRepository.Delete(id);
            await _productRepository.Commit();

            var productRemove = new DeleteProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };

            return (product is null)
            ? new BaseResponse<DeleteProductResponse>(null, 500, "[FX011] Failed to Remove product")
            : new BaseResponse<DeleteProductResponse>(productRemove, message: "Product successfully deleted");
        }




    }
}