using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Address;

namespace Application.Request.Person
{
    public class PersonGetByIdRequest
    {
        public Guid Id { get; set; }

    }
}