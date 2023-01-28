using Exam.DAL;
using Exam.Models;
using Exam.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Areas.Manage.Controllers
{
    public class AccountController : Controller
    {

        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
             _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVM logUser)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByNameAsync(logUser.UsernameOrEmail);

            if(user is null)
            {
                user = await _userManager.FindByEmailAsync(logUser.UsernameOrEmail);
                if(user is null)
                {
                    ModelState.AddModelError("", "Login or Password is worng");
                    return View();
                }
            }


            var result = await _signInManager.PasswordSignInAsync(user, logUser.Password,logUser.RememberMe,true);


            
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register()
        {
            return View();
        }



    }
}
