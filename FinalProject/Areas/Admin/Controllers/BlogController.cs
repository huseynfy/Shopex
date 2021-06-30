using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Helpers;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = " Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.BlogCount = _db.Blogs.Count();
            ViewBag.ContactUsCount = _db.ContactUS.Count();
            return View(_db.Blogs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (_db.Blogs.Count()!=3)
            {
            if (ModelState["Photo"].ValidationState==Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "This is not an image");
                return View();
            }
            if (blog.Photo.LessThan(2))
            {
                ModelState.AddModelError("Photo", "Image must be less than 2 mb");
                return View();
            }
            string filename =await blog.Photo.SavePhoto(_env.WebRootPath, "img/");
            blog.Image = filename;
            await _db.Blogs.AddAsync(blog);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            return Content("Sorry we've blocked your request!");
        }
        public async Task<IActionResult> Update(int id)
        {
            Blog blog = await _db.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            return View(blog);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Blog blog)
        {
            Blog blognews = await _db.Blogs.FindAsync(id);
            blognews.Commentator = blog.Commentator;
            blognews.Comments = blog.Comments;
            blognews.DateTime = blog.DateTime;
            blognews.Description = blog.Description;
            blognews.Title = blog.Title;
            if (blog.Photo!=null)
            {
                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "This is not an image");
                    return View();
                }
                if (blog.Photo.LessThan(2))
                {
                    ModelState.AddModelError("Photo", "Image must be less than 2 mb");
                    return View();
                }
                Blog blog1 = await _db.Blogs.FindAsync(id);
                FileHelper.DeleteImg(_env.WebRootPath, "img/", blog1.Image);
                string filename = await blog.Photo.SavePhoto(_env.WebRootPath, "img/");
                blog1.Image = filename;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Blog blog = await _db.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            return View(blog);
        }
        [HttpPost,ValidateAntiForgeryToken,ActionName("Delete")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (_db.Blogs.Count()!=1)
            {
            Blog blog = await _db.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            FileHelper.DeleteImg(_env.WebRootPath, "img/", blog.Image);
            _db.Blogs.Remove(blog);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            return Content("Sorry we've blocked your request!");
        }
    }
}