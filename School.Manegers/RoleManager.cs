using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace School.Manegers
{
   
    
        public class RoleManager : BaseManager<IdentityRole>
        {
            RoleManager<IdentityRole> roleManager;
            public RoleManager(SchoolDbContext context, RoleManager<IdentityRole> roleManager) : base(context)
            {
                this.roleManager = roleManager;
            }


            public async Task<IdentityResult> Add(string rolename)
            {
            var test = await roleManager.CreateAsync(new IdentityRole { Name = rolename });

            return test;
            }
        }
    
}
