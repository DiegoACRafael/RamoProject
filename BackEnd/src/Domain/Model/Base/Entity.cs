using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Model.Base
{
    public class Entity
    {
        public Guid Id { get; set; } = new Guid();
    }
}