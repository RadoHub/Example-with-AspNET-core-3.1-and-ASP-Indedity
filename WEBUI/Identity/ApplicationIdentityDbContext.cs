using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Identity
{
    public class ApplicationIdentityDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options): base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
