using Microsoft.AspNetCore.Mvc;
using profi.DAL;
using profi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profi.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel
            {
                Testimonials = _context.Testimonials.ToList()
            };
            return View(home);
        }
    }
}
