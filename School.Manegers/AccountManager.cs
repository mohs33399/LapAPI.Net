using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using LinqKit;
using Microsoft.IdentityModel.Tokens;
using School.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using School.ViewModels.Theacher;

namespace School.Manegers
{
  
    
        public class AccountManager : BaseManager<User>
        {
            private UserManager<User> UserManager;
            private SignInManager<User> signInManager;
            public AccountManager(
                SchoolDbContext context,
                UserManager<User> _UserManager,
                SignInManager<User> _signInManager
                )
                : base(context)
            {
                UserManager = _UserManager;
                signInManager = _signInManager;

            }



        public async Task<IdentityResult> Register(UserRegisterVM userRegister)
        {

            var res = await UserManager.CreateAsync(userRegister.ToModel(), userRegister.Password);
            if (res.Succeeded)
            {
                User user = await UserManager.FindByNameAsync(userRegister.UserName);

                res = await UserManager.AddToRoleAsync(user, userRegister.Role);

                if (userRegister.Role == "Teacher")
                {
                    //
                }
                //else if (userRegister.Role == "Client")
                //{
                //    //
                //}
            }
            return res;
        }


        public async Task<SignInResult> Login(UserLoginVM vmodel)
            {
                //if correct Email
                var User = await UserManager.FindByEmailAsync(vmodel.EmailOrUserName);
                if (User != null)
                    return await signInManager.PasswordSignInAsync(User, vmodel.Password, true, true);
                else
                    return await signInManager.PasswordSignInAsync(vmodel.EmailOrUserName, vmodel.Password, true, true);
            }





        public async Task<User> FindByUserName(string userName)
        {
            return await UserManager.FindByNameAsync(userName);
        }
        public async Task<User> FindByEmail(string email)
        {
            return await UserManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetUserRoles(User user)
        {
            return await UserManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AsignUserToRole(User user, string newrole)
        {
            return await UserManager.AddToRoleAsync(user, newrole);
        }


        public async Task Signout()
        {
            await signInManager.SignOutAsync();
        }
    }
    
}

