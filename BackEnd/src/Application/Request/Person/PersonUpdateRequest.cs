using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Address;

namespace Application.Request.Person
{
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage = "{0} and required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public int Age { get; set; }

        public string Cpf { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        [EmailAddress(ErrorMessage = "{0} invalid")]
        public string Email { get; set; }

        public AddressUpdateRequest Address { get; set; }
    }
}