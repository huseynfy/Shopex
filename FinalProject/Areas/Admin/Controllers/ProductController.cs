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
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        public ProductController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.ContactUsCount = _db.ContactUS.Count();
            var pro = _db.Products.Select(p=>new Product {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                Category = p.Category,
                Brand = p.Brand,
                CategoryId=p.CategoryId,
                BrandId=p.BrandId,
                ImageId=p.ImageId,
                Description=p.Description,
                Detail=p.Detail,
                Sale=p.Sale
            });
            return View(pro);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product pro)
        {
            await _db.Products.AddAsync(pro);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Product pro = await _db.Products.FindAsync(id);
            if (pro == null) return NotFound();
            return View(pro);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Product pro)
        {
            Product pro1 = await _db.Products.FindAsync(id);
            pro1.BrandId = pro.BrandId;
            pro1.Name = pro.Name;
            pro1.CategoryId = pro.CategoryId;
            pro1.ImageId = pro.ImageId;
            pro1.Price = pro.Price;
            pro1.Sale = pro.Sale;
            pro1.Detail = pro.Detail;
            pro1.Description = pro.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Product pro = await _db.Products.FindAsync(id);
            if (pro == null) return NotFound();
            return View(pro);
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeletePro(int id)
        {
            if (_db.Products.Count() != 1)
            {
                Product pro = await _db.Products.FindAsync(id);
                if (pro == null) return NotFound();
                _db.Products.Remove(pro);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return Content("Sorry we've blocked your request!");
        }
    }
}