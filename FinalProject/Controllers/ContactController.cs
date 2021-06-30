using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using System.Net.Mail;
using System.Net;

namespace FinalProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
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
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactUs contact)
        {
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c => c.Total);
            ViewBag.PriceCart = proprice;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please enter something!");
                ; HomeVM home = new HomeVM()
                {
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1)
                };
                return View(home);
            }
            await _db.ContactUS.AddAsync(contact);
            await _db.SaveChangesAsync();
            ViewBag.SuccessMsg = "Thanks , we will try to reach you as soon as possible!";
            return RedirectToAction("Index");
        }
    }
}