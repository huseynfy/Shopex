using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public BrandController(AppDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.ContactUsCount = _db.ContactUS.Count();
            return View(_db.Brands);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            await _db.Brands.AddAsync(brand);
            await _db.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Brand brand = await _db.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Brand brand)
        {
            Brand brand1 = await _db.Brands.FindAsync(id);
            brand1.Name = brand.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Brand brand = await _db.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }
        [HttpPost,ValidateAntiForgeryToken,ActionName("Delete")]
        public async Task<IActionResult> DeleteBrand(Brand brand)
        {
            _db.Brands.Remove(brand);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}