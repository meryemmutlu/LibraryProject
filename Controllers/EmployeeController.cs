using LibraryProject.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LibraryProject.Controllers
{
    [AllowAnonymous] //Kullanıcı girişi olmadan tüm sayfaları görmemize yarar.
                     //Kullanıcı girişi ile sayfalar görünür olsun isterseniz EmployeeController hariç diğer controllerlardan [AllowAnonymous]'u kaldırmanız gerekir.
    public class EmployeeController : Controller
    {
        LibraryContext db = new LibraryContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Employee e)
        {
            var datavalue = db.Employees.FirstOrDefault(x => x.UserName == e.UserName && x.Password == e.Password);
            if (datavalue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,e.UserName)
                };

                var useridentity = new ClaimsIdentity(claims, "Employee");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);


                return RedirectToAction("Index", "Home");
            }


            return View();
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LogIn", "Employee");

        }
    }
}
