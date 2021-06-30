using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _db;
        public CartController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            #region Cart Session Version
            //string cart = HttpContext.Session.GetString("cart");
            //return View(JsonConvert.DeserializeObject<List<BasketVM>>(cart));
            #endregion
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c=>c.Total);
            ViewBag.PriceCart =proprice;
            HomeVM home = new HomeVM()
            {
                Carts = _db.Carts.Where(c=>c.Username==User.Identity.Name),
                Contacts = _db.Contacts.First(c => c.Id == 1)
            };
            return View(home);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id,int number)
        {
            ViewBag.CartCount = _db.Carts.Where(c => c.Username == User.Identity.Name).Count();
            double proprice = _db.Carts.Where(c => c.Username == User.Identity.Name).Sum(c => c.Total);
            ViewBag.PriceCart = proprice;
            Product pro = await _db.Products.FindAsync(id);
            Image img = await _db.Images.FindAsync(pro.ImageId);
            string username = User.Identity.Name;

            Cart existpro = _db.Carts.Where(c => c.Username == User.Identity.Name).FirstOrDefault(c => c.ProId == id);
            Cart cart = new Cart();
            if (existpro == null)
            {
                cart.ProCount = number;
                cart.ProId = id;
                cart.ProImage = img.Image1;
                cart.ProPrice = pro.Price;
                cart.ProName = pro.Name;
                cart.Username = username;
                cart.Total = pro.Price * cart.ProCount;
                await _db.Carts.AddAsync(cart);
            }
            else
            {
                existpro.ProCount += number;
                existpro.Total = pro.Price * existpro.ProCount;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        #region Cart Session Version
        //Cart Session Version

        //public async Task<IActionResult> AddToCart(int id)
        //{
        //    Product pro = await _db.Products.FindAsync(id);
        //    //Image img = await _db.Images.FindAsync(pro.ImageId);
        //    if (pro == null) return NotFound();
        //    List<BasketVM> list;
        //    string currentcart=HttpContext.Session.GetString("cart");
        //    if (currentcart==null)
        //    {
        //        list = new List<BasketVM>();
        //    }
        //    else
        //    {
        //        list = JsonConvert.DeserializeObject<List<BasketVM>>(currentcart);
        //    }
        //    var checkpro = list.FirstOrDefault(c => c.Id == id);
        //    if (checkpro == null)
        //    {
        //        BasketVM cart1 = new BasketVM();
        //        cart1.Count = 1;
        //        cart1.Id = pro.Id;
        //        //cart1.Image = img.Image1;
        //        cart1.Price = pro.Price;
        //        cart1.Name = pro.Name;
        //        list.Add(cart1);
        //    }
        //    else
        //    {
        //        checkpro.Count += 1;
        //    }
        //    string cart = JsonConvert.SerializeObject(list);
        //    HttpContext.Session.SetString("cart", cart);

        //    return RedirectToAction(nameof(Index));
        //}


        //Cart Database Version
        #endregion
        public async Task<IActionResult> AddToCart(int id)
        {
            Product pro = await _db.Products.FindAsync(id);
            Image img = await _db.Images.FindAsync(pro.ImageId);
            string username = User.Identity.Name;
            
            Cart existpro = _db.Carts.Where(c => c.Username == User.Identity.Name).FirstOrDefault(c => c.ProId == id);
            Cart cart = new Cart();
            if (existpro==null)
            {
                cart.ProCount =1;
                cart.ProId = id;
                cart.ProImage = img.Image1;
                cart.ProPrice = pro.Price;
                cart.ProName = pro.Name;
                cart.Username = username;
                cart.Total = pro.Price * cart.ProCount;
                await _db.Carts.AddAsync(cart);
            }
            else
            {
                existpro.ProCount += 1;
                existpro.Total = pro.Price * existpro.ProCount;
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Cart cart = await _db.Carts.FindAsync(id);
            _db.Carts.Remove(cart);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}