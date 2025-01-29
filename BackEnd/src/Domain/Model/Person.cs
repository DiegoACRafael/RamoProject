using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Base;

namespace Domain.Model
{
    public class Person : Entity
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Idade")]
        public int Age { get; set; }

        [Display(Name = "Cpf")]
        public string CpfCnpj { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Endereco")]
        public virtual Address Address { get; set; }
    }
}