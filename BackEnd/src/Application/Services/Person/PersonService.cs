using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Address;
using Application.Request.Person;
using Application.Response;
using Application.Response.Address;
using Application.Response.Person;
using Domain.Model;
using Infra.EF.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PagedResponse<List<PersonGetAllResponse>>> GetAsync()
        {
            var request = new PersonGetAllRequest();

            var persons = await _personRepository.GetAllAsync(include: q => q.Include(p => p.Address));

            var response = persons.Select(x => new PersonGetAllResponse
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                Cpf = x.CpfCnpj,
                Email = x.Email,
                Address = x.Address != null ? new AddressGetAllResponse
                {
                    Street = x.Address.Street,
                    Number = x.Address.Number,
                    ZipCode = x.Address.ZipCode,
                    Neighborhood = x.Address.Neighborhood,
                    City = x.Address.City,
                    State = x.Address.State

                } : null
            });

            var paged = response.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            return (persons is null)
                ? new PagedResponse<List<PersonGetAllResponse>>(null, 500, "[FX052] There is no registered Person")
                : new PagedResponse<List<PersonGetAllResponse>>(paged, persons.Count(), request.PageNumber, request.PageSize, message: "List Persons");
        }



        public async Task<BaseResponse<PersonGetByIdResponse>> GetByIdAsync(Guid id)
        {

            var person = await _personRepository.GetByIdAsync(id,
                                                                include: p => p.Include(p => p.Address));

            var respons = new PersonGetByIdResponse
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age,
                Cpf = person.CpfCnpj,
                Email = person.Email,
                Address = person.Address != null ? new AddressGetByIdResponse
                {
                    Street = person.Address.Street,
                    Number = person.Address.Number,
                    ZipCode = person.Address.ZipCode,
                    Neighborhood = person.Address.Neighborhood,
                    City = person.Address.City,
                    State = person.Address.State

                } : null
            };

            return (person is null)
              ? new BaseResponse<PersonGetByIdResponse>(null, 404, "[FX042] Person does not exist")
              : new BaseResponse<PersonGetByIdResponse>(respons, message: "Successfully located");
        }

        public async Task<BaseResponse<CreatePersonResponse>> CreateAsync(CreatePersonRequest request)
        {

            var _person = await _personRepository.FirstOrDefaultAsync(p => p.CpfCnpj == request.Cpf);

            if (_person != null)
                throw new Exception("User exists");

            var person = new Person
            {
                Name = request.Name,
                Age = request.Age,
                CpfCnpj = request.Cpf,
                Email = request.Email,
                Address = request.Address != null ? MapAddress(request.Address) : null
            };

            var personAdd = await _personRepository.Create(person);
            await _personRepository.Commit();

            var personCreate = new CreatePersonResponse
            {
                Id = person.Id,
                Name = personAdd.Name,
                Age = personAdd.Age,
                CpfCnpj = personAdd.CpfCnpj,
                Email = person.Email,
                Address = request.Address != null ? new CreateAddressResponse
                {
                    Street = request.Address.Street,
                    Number = request.Address.Number,
                    ZipCode = request.Address.ZipCode,
                    Neighborhood = request.Address.Neighborhood,
                    City = request.Address.City,
                    State = request.Address.State

                } : null
            };

            return personCreate is null
           ? new BaseResponse<CreatePersonResponse>(null, 500, message: "[FX032] Failure to create Person")
           : new BaseResponse<CreatePersonResponse>(personCreate, message: "Successfully created Person");
        }

        public async Task<BaseResponse<PersonUpdateResponse>> UpdateAsync(Guid id, PersonUpdateRequest request)
        {
            var person = await _personRepository.GetByIdAsync(id, include: p => p.Include(x => x.Address));

            person.Name = request.Name;
            person.Age = request.Age;
            person.Email = request.Email;
            person.CpfCnpj = request.Cpf;

            if (request.Address != null)
            {
                person.Address.Street = request.Address.Street;
                person.Address.Number = request.Address.Number;
                person.Address.ZipCode = request.Address.ZipCode;
                person.Address.Neighborhood = request.Address.Neighborhood;
                person.Address.City = request.Address.City;

            }

            var personUp = await _personRepository.Update(person);
            await _personRepository.Commit();

            var response = new PersonUpdateResponse
            {
                Name = person.Name,
                Age = person.Age,
                CpfCnpj = person.CpfCnpj,
                Email = person.Email,
                Address = person.Address != null ? new AddressUpdateResponse
                {
                    Street = person.Address.Street,
                    Number = person.Address.Number,
                    ZipCode = person.Address.ZipCode,
                    Neighborhood = person.Address.Neighborhood,
                    City = person.Address.City,
                    State = person.Address.State
                } : null
            };


            return (person is null)
                ? new BaseResponse<PersonUpdateResponse>(null, 500, "[FX022] Failure to update Person")
                : new BaseResponse<PersonUpdateResponse>(response, message: "Person successfully updated");
        }

        public async Task<BaseResponse<PersonDeleteResponse>> DeleteAsync(Guid id)
        {
            var person = await _personRepository.GetById(id);

            if (person != null)
            {
                await _personRepository.Delete(person.Id);
                await _personRepository.Commit();
            }


            var response = new PersonDeleteResponse
            {
                Id = id,
                Name = person.Name,
                Age = person.Age,
                Email = person.Email,
            };
            return (person is null)
               ? new BaseResponse<PersonDeleteResponse>(null, 500, "[FX011] Failed to Remove Person")
               : new BaseResponse<PersonDeleteResponse>(response, message: "Person successfully deleted");

        }

        private Address MapAddress(CreateAddressRequest address)
            => new Address
            {
                Street = address.Street,
                Number = address.Number,
                ZipCode = address.ZipCode,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State
            };
    }
}