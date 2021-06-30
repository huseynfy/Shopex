using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
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
                Sliders = _db.Sliders,
                SliderTexts = _db.SliderTexts.First(s => s.Id == 1),
                Products =_db.Products.Select(p=>new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Category = p.Category,
                    Brand=p.Brand
                }),
                PromotedProducts=_db.PromotedProducts.First(p=>p.Id==1),
                Carts=_db.Carts.Where(c => c.Username == User.Identity.Name),
                Contacts = _db.Contacts.First(c => c.Id == 1)
            };
            return View(home);
        }
        //Email
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(string email)
        {
            if (email!=null)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Huseyn Yusifov", "huseynfy@gmail.com"));
                message.To.Add(new MailboxAddress(email));
                message.Subject = "Hello";
                message.Body = new TextPart("plain")
                {
                    Text = "Thanks for subscription!"
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("huseynfy@gmail.com", "12345@As");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}