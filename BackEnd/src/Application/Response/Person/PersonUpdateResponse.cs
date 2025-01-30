using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Response.Address;

namespace Application.Response.Person
{
    public class PersonUpdateResponse
    {
        
        public string Name { get; set; }
        public int Age { get; set; }
        public string CpfCnpj { get; set; }
        public string Email { get; set; }
        public AddressUpdateResponse Address { get; set; }
    }
}