using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EF.Data.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Street);
            builder.Property(x => x.Number);
            builder.Property(x => x.ZipCode);
            builder.Property(x => x.Neighborhood);
            builder.Property(x => x.City);
            builder.Property(x => x.State);

            builder.HasOne(e => e.Person)
             .WithOne(p => p.Address)
             .HasForeignKey<Address>(p => p.PersonId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}