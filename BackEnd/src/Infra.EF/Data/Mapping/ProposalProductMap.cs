using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EF.Data.Mapping
{
    public class ProposalProductMap : IEntityTypeConfiguration<ProposalProduct>
    {
        public void Configure(EntityTypeBuilder<ProposalProduct> builder)
        {
            builder.HasKey(p => new { p.ProductId, p.ProposalId });

            builder.Ignore(p => p.Id);

            builder.HasOne(p => p.Product)
                    .WithMany(p => p.ProposalProducts)
                    .HasForeignKey(p => p.ProductId);

            builder.HasOne(p => p.Proposal)
                    .WithMany(p => p.ProposalProducts)
                    .HasForeignKey(p => p.ProposalId);

            builder.ToTable("ProposalProducts");
        }
    }
}