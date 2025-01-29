using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.EF.Data.Context
{
    public class AppDataContext : IdentityDbContext

    // public class AppDataContext : IdentityDbContext<ApplicationUser,
    //                                               ApplicationRole,
    //                                               Guid,
    //                                               ApplicationUserClaim,
    //                                               ApplicationUserRole,
    //                                               ApplicationUserLogin,
    //                                               ApplicationRoleClaim,
    //                                               ApplicationUserToken>
    {

        // public DbSet<Person> Persons { get; set; }
        // public DbSet<Address> Addresses { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.ApplyConfiguration(new PersonMap());
        //     modelBuilder.ApplyConfiguration(new AddressMap());
        // }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }
    }
}