using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderTextController : Controller
    {
        private readonly AppDbContext _db;
        public SliderTextController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.ContactUsCount = _db.ContactUS.Count();
            return View(_db.SliderTexts.First(s=>s.Id==1));
        }
        public IActionResult Update()
        {
            return View(_db.SliderTexts.First(s => s.Id == 1));
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SliderText text)
        {
            _db.Entry(text).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}