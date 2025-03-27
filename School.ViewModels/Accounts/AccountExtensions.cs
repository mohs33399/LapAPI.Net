using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace School.ViewModels.Accounts
{
    public static class AccountExtensions
    {
        public static User ToModel(this UserRegisterVM viewmodel)
        {
            return new User
            {
                UserName = viewmodel.UserName,
                Email = viewmodel.Email,
                PhoneNumber = viewmodel.PhoneNumber,
            };
        }
    }
}
