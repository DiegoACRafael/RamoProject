using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Configurations
{
    public class IdentityConfigurations
    {
        public class ApplicationUser : IdentityUser<Guid> { }
        public class ApplicationRole : IdentityRole<Guid> { }
        public class ApplicationRoleClaim : IdentityRoleClaim<Guid> { }
        public class ApplicationUserClaim : IdentityUserClaim<Guid> { }
        public class ApplicationUserLogin : IdentityUserLogin<Guid> { }
        public class ApplicationUserRole : IdentityUserRole<Guid> { }
        public class ApplicationUserToken : IdentityUserToken<Guid> { }
    }
}