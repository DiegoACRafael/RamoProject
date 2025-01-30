using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Response.Address;

namespace Application.Response.Person
{
    public class PersonGetByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

        public AddressGetByIdResponse Address { get; set; }
    }
}