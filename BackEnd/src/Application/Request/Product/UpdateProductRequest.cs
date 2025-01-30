using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Request.Product
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "{0} and required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "{0} and required")]
        public string Description { get; set; }
    }
}