using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using School.Manegers;

using Microsoft.AspNetCore.Identity;
using School.ViewModels.Accounts;
using School.ViewModels.Theacher;
using ConsoleApp1;
using LinqKit;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;



namespace School.services
{
    public class AccountServices
    {
        AccountManager accountManager;
       TeacherManager TeacherManager;
        //ClientManager clientManager;
        IConfiguration appSettingConfiguration;
        public AccountServices(
            AccountManager _accountManager,
            TeacherManager _TeacherManager,
           // ClientManager _clientManager,
            IConfiguration configuration
            )
        {
            accountManager = _accountManager;
            TeacherManager = _TeacherManager;
            //clientManager = _clientManager;
            appSettingConfiguration = configuration;
        }

        public async Task<IdentityResult> CreateAccount(UserRegisterVM user)
        {
            var userRes = await accountManager.Register(user);

            if (userRes.Succeeded)
            {
                var currentUser = await accountManager.FindByUserName(user.UserName);
                if (user.Role == "Teacher")
                {
                    //Add Record In Teacher table
                    TeacherManager.Add(new Teacher() { TeacherId  = int.Parse(currentUser.Id) });
                    return IdentityResult.Success;
                }
                //else if (user.Role == "Client")
                //{
                //    //Add Record In Client table
                //    clientManager.Add(new Client { UserId = currentUser.Id });
                //    return IdentityResult.Success;
                //}

            }
            return IdentityResult.Failed();
        }

        public async Task<SignInResult> Login(UserLoginVM user)
        {
            return await accountManager.Login(user);
        }

        public async Task<string> LoginWithToken(UserLoginVM user)
        {
            var res = await accountManager.Login(user);
            if (res.Succeeded)
            {
                //give me data to be encrpted in token
                List<Claim> claims = new List<Claim>();
                var currentUser = await accountManager.FindByUserName(user.EmailOrUserName);
                if (currentUser == null)
                {
                    currentUser = await accountManager.FindByEmail(user.EmailOrUserName);
                }
                var roles = await accountManager.GetUserRoles(currentUser);

                claims.Add(new Claim(ClaimTypes.Name, currentUser.UserName));
                claims.Add(new Claim(ClaimTypes.Email, currentUser.Email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, currentUser.Id));
                roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

                //make token    =>      JWT 

                JwtSecurityToken securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(
                        algorithm: SecurityAlgorithms.HmacSha256,
                        key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettingConfiguration["JWT:PrivateKey"]))
                    )
                );
                return new JwtSecurityTokenHandler().WriteToken(securityToken);

            }
            else if (res.IsLockedOut || res.IsNotAllowed)
            {
                return string.Empty;
            }
            return null;

        }
        public async Task Signout()
        {
            await accountManager.Signout();
        }


    }
}
