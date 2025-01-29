using System;
using Domain.Model;
using Infra.EF.Data.Mapping;
using Infra.EF.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.EF.Data.Context
{
    public class AppDataContext : IdentityDbContext<ApplicationUser,
                                                  ApplicationRole,
                                                  Guid,
                                                  ApplicationUserClaim,
                                                  ApplicationUserRole,
                                                  ApplicationUserLogin,
                                                  ApplicationRoleClaim,
                                                  ApplicationUserToken>
    {
        public AppDataContext()
        {
        }

        public AppDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());
        }
    }
}