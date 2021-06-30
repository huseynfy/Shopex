using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace FinalProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;
        private  RoleManager<IdentityRole> _rolemanager;
        private  SignInManager<AppUser> _signinmanager;
        private  UserManager<AppUser> _usermanager;
        public LoginController(AppDbContext db,SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _rolemanager = roleManager;
            _signinmanager = signInManager;
            _usermanager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c => c.Total);
            ViewBag.PriceCart = proprice;
            HomeVM home = new HomeVM()
            {
                Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                Contacts = _db.Contacts.First(c => c.Id == 1)
            };
            return View(home);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(HomeVM login)
        {
            if (ModelState["Password"].ValidationState==Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid
                && ModelState["Username"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                HomeVM home = new HomeVM()
                {
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1)
                };
                return View(home);
            }
            AppUser user =await _usermanager.FindByNameAsync(login.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Password is wrong");
                HomeVM home = new HomeVM()
                {
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1)
                };
                return View(home);
            }
            SignInResult result= await _signinmanager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is wrong");
            HomeVM home = new HomeVM()
            {
                Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                Contacts = _db.Contacts.First(c => c.Id == 1)
            };
            return View(home);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task RoleSeed()
        {
            if (! await _rolemanager.RoleExistsAsync(UR.Roles.Admin.ToString()))
            {
                await _rolemanager.CreateAsync(new IdentityRole(UR.Roles.Admin.ToString()));
            }
            if (! await _rolemanager.RoleExistsAsync(UR.Roles.Member.ToString()))
            {
                await _rolemanager.CreateAsync(new IdentityRole(UR.Roles.Member.ToString()));
            }
        }
        public IActionResult Register()
        {
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c => c.Total);
            ViewBag.PriceCart = proprice;
            HomeVM home = new HomeVM()
            {
                Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                Contacts = _db.Contacts.First(c => c.Id == 1)
            };
            return View(home);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(HomeVM register)
        {
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c => c.Total);
            ViewBag.PriceCart = proprice;
            if (!ModelState.IsValid)
            {
                HomeVM home = new HomeVM()
                {
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1)
                };
                return View(home);
            }
            AppUser user = new AppUser
            {
                Name = register.Name,
                Surname = register.Surname,
                Email = register.Email,
                UserName = register.Username
            };
            IdentityResult result= await _usermanager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                HomeVM home = new HomeVM()
                {
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1)
                };
                return View(home);
            }
            await _usermanager.AddToRoleAsync(user, UR.Roles.Member.ToString());
            await _signinmanager.SignInAsync(user, true);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {
            await _signinmanager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
        //public async Task<IActionResult> UserAccount(string id)
        //{
        //    AppUser user = await _db.Users.FindAsync(id);
        //    //if (user == null) return NotFound();
        //    HomeVM home = new HomeVM()
        //    {
        //        Carts = _db.Carts,
        //        AppUser=user
        //    };
        //    return View(home);
        //}
        //[HttpPost,ValidateAntiForgeryToken,ActionName("UserAccount")]
        //public async Task<IActionResult> ChangeUserName(string id, AppUser username1)
        //{
        //    AppUser user = await _db.Users.FindAsync(id);
        //    user.UserName = username1.UserName;
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
    }
}