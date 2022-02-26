using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace microkart.identity.Data
{
    public class MicrokartIdentityDbContext : IdentityDbContext
    {
        public MicrokartIdentityDbContext(DbContextOptions<MicrokartIdentityDbContext> options) : base(options)
        {

        }
    }
}
