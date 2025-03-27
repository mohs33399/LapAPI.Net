using Microsoft.AspNetCore.Mvc;
using School.ViewModels.Accounts;
using School.ViewModels;
using School.Manegers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private RoleManager roleManager;


        private AccountManager accountManager;
        public AccountController(AccountManager _accountManager ,RoleManager _roleManager)
        {
            accountManager = _accountManager;
            this.roleManager = _roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var list = roleManager.GetList(r => r.Name != "Admin")
             .Select(r => new SelectListItem(r.Name, r.Name)).ToList();
            ViewData["roles"] = list;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM user)
        {
            if (ModelState.IsValid)
            {
                var res = await accountManager.Register(user);
                if (res.Succeeded)
                {
                    return RedirectToAction("login");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View();
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM vmodel)
        {
            if (ModelState.IsValid)
            {
                var res = await accountManager.Login(vmodel);
                if (res.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                else if (res.IsLockedOut || res.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Sorry try again Later!!!!");
                }
                else
                {
                    ModelState.AddModelError("", "Sorry Invalid Email Or User Name Or Password");
                }
                return View();
            }
            return View();

        }
    }
}
