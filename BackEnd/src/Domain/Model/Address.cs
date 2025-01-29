using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Base;

namespace Domain.Model
{
    public class Address : Entity
    {
        [Display(Name = "Pessoa Id")]
        public Guid PersonId { get; set; }

        [Display(Name = "Rua")]
        public string Street { get; set; }

        [Display(Name = "Numero")]
        public string Number { get; set; }

        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [Display(Name = "Caixa-Posta ")]
        public string ZipCode { get; set; }

        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        public string State { get; set; }

        [Display(Name = "Pessoa")]
        public virtual Person Person { get; set; }
    }
}