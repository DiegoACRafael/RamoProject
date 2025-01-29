using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Request.Address
{
    public class AddressUpdateRequest
    {
        [Required(ErrorMessage = "{0} and required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public string Number { get; set; }

        public string Neighborhood { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}