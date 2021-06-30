using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class CheckOutController : Controller
    {
        private AppDbContext _db;
        public CheckOutController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c => c.Total);
            ViewBag.PriceCart = proprice;
            HomeVM home = new HomeVM
            {
                Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                Contacts = _db.Contacts.First(c => c.Id == 1)
            };
            return View(home);
        }
    }
}