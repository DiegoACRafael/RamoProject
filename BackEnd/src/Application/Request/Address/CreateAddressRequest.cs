using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Request.Address
{
    public class CreateAddressRequest
    {
        public Guid PersonId { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public string Number { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public string State { get; set; }
    }
}