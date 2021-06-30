using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles =" Admin")]
    public class DashBoardController : Controller
    {
        private readonly AppDbContext _db;
        public DashBoardController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.ContactUsCount = _db.ContactUS.Count();
            return View(_db.ContactUS);
        }
    }
}