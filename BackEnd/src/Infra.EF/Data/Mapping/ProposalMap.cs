using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EF.Data.Mapping
{
    public class ProposalMap : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.Person)
                    .WithMany()
                    .HasForeignKey(p => p.PersnId);

            builder.ToTable("Proposals");
        }
    }
}