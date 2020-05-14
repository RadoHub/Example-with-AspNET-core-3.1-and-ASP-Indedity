using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Identity
{
    public class ApplicationRole: IdentityRole<int>
    {
        public ApplicationRole()
        {

        }
        public ApplicationRole(string roleName): base(roleName)
        {

        }
    }
}
