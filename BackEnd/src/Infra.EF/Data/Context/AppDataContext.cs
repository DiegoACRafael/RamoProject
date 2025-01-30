using Domain.Model;
using Infra.EF.Data.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.EF.Data.Context
{
    public class AppDataContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProposalProduct> ProposalProducts { get; set; }
        public DbSet<Proposal> Proposals { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PersonMap());
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new ProposalProductMap());
            builder.ApplyConfiguration(new ProposalMap());
            builder.ApplyConfiguration(new ProductMap());

            base.OnModelCreating(builder);
        }
    }
}