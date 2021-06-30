using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
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
                Carts=_db.Carts.Where(c => c.Username == User.Identity.Name),
                Blogs=_db.Blogs,
                Contacts = _db.Contacts.First(c => c.Id == 1)
            };
            return View(home);
        }
    }
}