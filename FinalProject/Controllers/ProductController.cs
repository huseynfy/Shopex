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
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        public ProductController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(int? page,int? categoryid,int? brandid)
        {
            //Category selection
            Category category =await _db.Categories.FindAsync(categoryid);

            //Brand Selection
            Brand brand = await _db.Brands.FindAsync(brandid);
            //Viewbags
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c => c.Total);
            ViewBag.PriceCart = proprice;
            ViewBag.PageCount = Math.Ceiling((decimal)_db.Products.Count() / 9);

            //Pagination start
            if (page==null && category==null && brand==null)
            {
                ViewBag.Page = 0;
                HomeVM home = new HomeVM
                {
                    Products = _db.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Image = p.Image,
                        Category = p.Category,
                        Brand = p.Brand
                    }).Take(9),
                    Brands = _db.Brands,
                    Categories = _db.Categories,
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1),
                    ProductsMini = _db.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Image = p.Image,
                        Category = p.Category,
                        Brand = p.Brand
                    }).Skip(5).Take(3)
                };
                return View(home);
            }
            //Category selection
            if (category!=null)
            {
                HomeVM home2 = new HomeVM
                {
                    Products = _db.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Image = p.Image,
                        Category = p.Category,
                        Brand = p.Brand
                    }).Where(p=>p.Category==category).Take(9),
                    Brands = _db.Brands,
                    Categories = _db.Categories,
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1),
                    ProductsMini = _db.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Image = p.Image,
                        Category = p.Category,
                        Brand = p.Brand
                    }).Skip(5).Take(3)
                };
                return View(home2);
            }
            //Brand Selection
            if (brand!=null)
            {
                HomeVM home3 = new HomeVM
                {
                    Products = _db.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Image = p.Image,
                        Category = p.Category,
                        Brand = p.Brand
                    }).Where(p => p.Brand == brand).Take(9),
                    Brands = _db.Brands,
                    Categories = _db.Categories,
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1),
                    ProductsMini = _db.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Image = p.Image,
                        Category = p.Category,
                        Brand = p.Brand
                    }).Skip(5).Take(3)
                };
                return View(home3);
            }
            //Pagination
            ViewBag.Page = page;
            HomeVM home1 = new HomeVM
            {
                Products = _db.Products.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Category = p.Category,
                    Brand = p.Brand
                }).Skip(9*(int)page).Take(9),
                Brands = _db.Brands,
                Categories = _db.Categories,
                Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                Contacts = _db.Contacts.First(c => c.Id == 1),
                ProductsMini= _db.Products.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Category = p.Category,
                    Brand = p.Brand
                }).Skip(5).Take(3)
            };
            return View(home1);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(string search,int? pricelow,int? pricehigh)
        {
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c => c.Total);
            ViewBag.PriceCart = proprice;

            ViewBag.Page = 0;
            ViewBag.PageCount = Math.Ceiling((decimal)_db.Products.Count() / 10);
            //If there is no request
            if (search == null && pricelow==null && pricehigh==null)
            {
                ModelState.AddModelError("", "Please enter something!");
                HomeVM home1 = new HomeVM
                {
                    Products = _db.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Image = p.Image,
                        Category = p.Category,
                        Brand = p.Brand
                    }).Take(9),
                    Brands = _db.Brands,
                    Categories = _db.Categories,
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1),
                    ProductsMini = _db.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Image = p.Image,
                        Category = p.Category,
                        Brand = p.Brand
                    }).Skip(5).Take(3)
                };
                return View(home1);
            }
            //Price sort
            if (pricelow!=null && pricehigh!=null)
            {
                HomeVM home1 = new HomeVM
                {
                    Products = _db.Products.Where(p=>p.Price>pricelow && p.Price<pricehigh).Take(9)
              .Select(p => new Product
              {
                  Id = p.Id,
                  Name = p.Name,
                  Price = p.Price,
                  Image = p.Image,
                  Category = p.Category,
                  Brand = p.Brand
              }).Take(9),
                    Brands = _db.Brands,
                    Categories = _db.Categories,
                    Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                    Contacts = _db.Contacts.First(c => c.Id == 1),
                    ProductsMini = _db.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Image = p.Image,
                        Category = p.Category,
                        Brand = p.Brand
                    }).Skip(5).Take(3)
                };
                return View(home1);
            }
            //Search system
            HomeVM home = new HomeVM
            {
                Products = _db.Products.Where(p => p.Name.Contains(search) || p.Brand.Name.Contains(search)
                ||p.Category.Name.Contains(search)).Take(9)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Category = p.Category,
                    Brand = p.Brand
                }).Take(9),
                Brands = _db.Brands,
                Categories = _db.Categories,
                Carts = _db.Carts.Where(c => c.Username == User.Identity.Name),
                Contacts = _db.Contacts.First(c => c.Id == 1),
                ProductsMini = _db.Products.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Category = p.Category,
                    Brand = p.Brand
                }).Skip(5).Take(3)
            };
            return View(home);
        }

        public async Task<IActionResult> Detail(int id,int imgid)
        {
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c => c.Total);
            ViewBag.PriceCart = proprice;

            Product pro = await _db.Products.FindAsync(id);
            Image image = await _db.Images.FindAsync(imgid);
            Brand brand = await _db.Brands.FindAsync(pro.BrandId);
            HomeVM home = new HomeVM
            {
                Product = pro,
                Products = _db.Products.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Category = p.Category,
                    Brand = brand
                }),
                Brands = _db.Brands,
                Categories = _db.Categories,
                Image=image,
                ProductReviews=_db.ProductReviews,
                Carts=_db.Carts.Where(c => c.Username == User.Identity.Name),
                Contacts = _db.Contacts.First(c => c.Id == 1)
            };
            return View(home);
        }
    }
}