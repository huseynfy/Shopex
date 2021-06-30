using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.Extentions;
using FinalProject.Helpers;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PromotedProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public PromotedProductController(AppDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.ContactUsCount = _db.ContactUS.Count();
            return View(_db.PromotedProducts.First(p=>p.Id==1));
        }
        public IActionResult Update()
        {
            return View(_db.PromotedProducts.First(p => p.Id == 1));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PromotedProduct prom)
        {
            if (!prom.PhotoBig.IsImage())
            {
                ModelState.AddModelError("PhotoBig", "This is not an image");
                return View();
            }
            if (prom.PhotoBig.LessThan(2))
            {
                ModelState.AddModelError("PhotoBig", "Image must be less than 2 mb");
                return View();
            }
            PromotedProduct imagepro = _db.PromotedProducts.First(p => p.Id == 1);
            FileHelper.DeleteImg(_env.WebRootPath, "img/", imagepro.ImageBig);
            string filename = await prom.PhotoBig.SavePhoto(_env.WebRootPath, "img/");
            imagepro.ImageBig = filename;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Update1()
        {
            return View(_db.PromotedProducts.First(p => p.Id == 1));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update1(PromotedProduct prom)
        {
            if (!prom.PhotoMedium.IsImage())
            {
                ModelState.AddModelError("PhotoMedium", "This is not an image");
                return View();
            }
            if (prom.PhotoMedium.LessThan(2))
            {
                ModelState.AddModelError("PhotoMedium", "Image must be less than 2 mb");
                return View();
            }
            PromotedProduct imagepro = _db.PromotedProducts.First(p => p.Id == 1);
            FileHelper.DeleteImg(_env.WebRootPath, "img/", imagepro.ImageMedium);
            string filename = await prom.PhotoMedium.SavePhoto(_env.WebRootPath, "img/");
            imagepro.ImageMedium = filename;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Update2()
        {
            return View(_db.PromotedProducts.First(p => p.Id == 1));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update2(PromotedProduct prom)
        {
            if (!prom.PhotoSmall1.IsImage())
            {
                ModelState.AddModelError("PhotoSmall1", "This is not an image");
                return View();
            }
            if (prom.PhotoSmall1.LessThan(2))
            {
                ModelState.AddModelError("PhotoSmall1", "Image must be less than 2 mb");
                return View();
            }
            PromotedProduct imagepro = _db.PromotedProducts.First(p => p.Id == 1);
            FileHelper.DeleteImg(_env.WebRootPath, "img/", imagepro.ImageSmall1);
            string filename = await prom.PhotoSmall1.SavePhoto(_env.WebRootPath, "img/");
            imagepro.ImageSmall1 = filename;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Update3()
        {
            return View(_db.PromotedProducts.First(p => p.Id == 1));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update3(PromotedProduct prom)
        {
            if (!prom.PhotoSmall2.IsImage())
            {
                ModelState.AddModelError("PhotoSmall2", "This is not an image");
                return View();
            }
            if (prom.PhotoSmall2.LessThan(2))
            {
                ModelState.AddModelError("PhotoSmall2", "Image must be less than 2 mb");
                return View();
            }
            PromotedProduct imagepro = _db.PromotedProducts.First(p => p.Id == 1);
            FileHelper.DeleteImg(_env.WebRootPath, "img/", imagepro.ImageSmall2);
            string filename = await prom.PhotoSmall2.SavePhoto(_env.WebRootPath, "img/");
            imagepro.ImageSmall2 = filename;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}