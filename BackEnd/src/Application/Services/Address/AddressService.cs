using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Address;
using Application.Response;
using Application.Response.Address;
using Domain.Model;
using Infra.EF.Interfaces;
using Infra.EF.Repositories;
using Microsoft.CodeAnalysis.CSharp;

namespace Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRespository;

        public AddressService(IAddressRepository addressRespository)
        {
            _addressRespository = addressRespository;
        }

        public async Task<PagedResponse<List<AddressGetAllResponse>>> GetAsync()
        {
            var request = new AddressGetAllRequest();

            var addresses = await _addressRespository.GetAll();

            var response = addresses.Select(a => new AddressGetAllResponse
            {
                PersonId = a.Id,
                Street = a.Street,
                Number = a.Number,
                ZipCode = a.ZipCode,
                Neighborhood = a.Neighborhood,
                City = a.City,
                State = a.State

            }).ToList();

            var paged = response.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            return (addresses is null)
                ? new PagedResponse<List<AddressGetAllResponse>>(null, 500, "[FX052] There is no registered address")
                : new PagedResponse<List<AddressGetAllResponse>>(paged, addresses.Count, request.PageNumber, request.PageSize, message: "List Address");
        }

        public async Task<BaseResponse<AddressGetByIdResponse>> GetByIdAsync(Guid id)
        {
            var address = await _addressRespository.GetById(id);

            var response = new AddressGetByIdResponse
            {

                Id = address.Id,
                Street = address.Street,
                Number = address.Number,
                ZipCode = address.ZipCode,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State

            };

            return (address is null)
              ? new BaseResponse<AddressGetByIdResponse>(null, 404, "[FX042] Address does not exist")
              : new BaseResponse<AddressGetByIdResponse>(response, message: "Successfully located");
        }

        public async Task<BaseResponse<CreateAddressResponse>> CreateAsync(CreateAddressRequest request)
        {
            Address addressAdd = await _addressRespository.FirstOrDefaultAsync(where: a => a.PersonId == request.PersonId);

            if (addressAdd != null)
            {
                MapAddress(addressAdd, request);
                await _addressRespository.Update(addressAdd);
            }
            else
            {
                addressAdd = new Address();
                MapAddress(addressAdd, request);
                await _addressRespository.Create(addressAdd);
            }

            await _addressRespository.Commit();

            var response = new CreateAddressResponse
            {
                personId = addressAdd.PersonId,
                Street = addressAdd.Street,
                Number = addressAdd.Number,
                ZipCode = addressAdd.ZipCode,
                Neighborhood = addressAdd.Neighborhood,
                City = addressAdd.City,
                State = addressAdd.State
            };

            return response is null
           ? new BaseResponse<CreateAddressResponse>(null, 500, message: "[FX032] Failure to create Address")
           : new BaseResponse<CreateAddressResponse>(response, message: "Successfully created Address");
        }

        private void MapAddress(Address address, CreateAddressRequest request)
        {
            address.PersonId = request.PersonId;
            address.Street = request.Street;
            address.Number = request.Number;
            address.ZipCode = request.ZipCode;
            address.Neighborhood = request.Neighborhood;
            address.City = request.City;
            address.State = request.State;
        }

        public async Task<BaseResponse<AddressUpdateResponse>> UpdateAsync(Guid id, AddressUpdateRequest request)
        {
            var address = await _addressRespository.GetById(id);

            address.Street = request.Street;
            address.Number = request.Number;
            address.ZipCode = request.ZipCode;
            address.Neighborhood = request.Neighborhood;
            address.City = request.City;
            address.State = request.State;

            var addressUp = await _addressRespository.Update(address);
            await _addressRespository.Commit();

            var response = new AddressUpdateResponse
            {
                Street = address.Street,
                Number = address.Number,
                ZipCode = address.ZipCode,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State

            };

            return (address is null)
                ? new BaseResponse<AddressUpdateResponse>(null, 500, "[FX022] Failure to update Person")
                : new BaseResponse<AddressUpdateResponse>(response, message: "Person successfully updated");
        }

        public async Task<BaseResponse<AddressDeleteResponse>> DeleteAsync(Guid id)
        {
            var address = await _addressRespository.GetById(id);

            if (address != null)
            {
                await _addressRespository.Delete(address.Id);
                await _addressRespository.Commit();
            }

            var response = new AddressDeleteResponse
            {
                PersonId = id,
                Street = address.Street,
                Number = address.Number,
                ZipCode = address.ZipCode,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State
            };
            return (address is null)
               ? new BaseResponse<AddressDeleteResponse>(null, 500, "[FX011] Failed to Remove Person")
               : new BaseResponse<AddressDeleteResponse>(response, message: "Person successfully deleted");
        }

    }
}