using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.ContactUsCount = _db.ContactUS.Count();
            return View(_db.Contacts.First(c => c.Id == 1));
        }
        public IActionResult Update()
        {
            return View(_db.Contacts.First(c => c.Id == 1));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Contact contact)
        {
            _db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}