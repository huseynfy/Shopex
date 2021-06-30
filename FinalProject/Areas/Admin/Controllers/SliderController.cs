using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Extentions;
using Microsoft.AspNetCore.Hosting;
using FinalProject.Helpers;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = " Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.ContactUsCount = _db.ContactUS.Count();
            ViewBag.ImageCount = _db.Sliders.Count();
            return View(_db.Sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slide)
        {
            if (_db.Sliders.Count()!=1)
            {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!slide.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "This is not an image");
                return View();
            }
            if (slide.Photo.LessThan(2))
            {
                ModelState.AddModelError("Photo", "Image must be less than 2 mb");
                return View();
            }
            string filename = await slide.Photo.SavePhoto(_env.WebRootPath, "img/");
            slide.Image = filename;
            await _db.Sliders.AddAsync(slide);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            return Content("We've blocked your request");
        }
        public async Task<IActionResult> Update(int id)
        {
            Slider slide = await _db.Sliders.FindAsync(id);
            if (slide == null) return NotFound();
            return View(slide);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Slider slide)
        {
            if (!slide.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "This is not an image");
                return View();
            }
            if (slide.Photo.LessThan(2))
            {
                ModelState.AddModelError("Photo", "Image must be less than 2 mb");
                return View();
            }
            Slider slide1 = await _db.Sliders.FindAsync(id);
            FileHelper.DeleteImg(_env.WebRootPath, "img/", slide1.Image);
            string filename = await slide.Photo.SavePhoto(_env.WebRootPath, "img/");
            slide1.Image = filename;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Slider slide = await _db.Sliders.FindAsync(id);
            if (slide == null) return NotFound();
            return View(slide);
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteImg(int id)
        {
            if (_db.Sliders.Count() != 1)
            {
                Slider slide1 = await _db.Sliders.FindAsync(id);
                if (slide1 == null) return NotFound();
                FileHelper.DeleteImg(_env.WebRootPath, "img/", slide1.Image);
                _db.Sliders.Remove(slide1);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return Content("Sorry we've blocked your request!");
        }
    }
}