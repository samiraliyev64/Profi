using Microsoft.AspNetCore.Mvc;
using profi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using profi.Helpers;
using Microsoft.AspNetCore.Hosting;
using profi.Models;
using Microsoft.AspNetCore.Authorization;

namespace profi.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = nameof(Role.RoleType.Admin) + "," + nameof(Role.RoleType.Member))]
    public class TestimonialsController : Controller
    {
        private AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public TestimonialsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //Read
        public IActionResult Index()
        {
            return View(_context.Testimonials);
        }
        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var testimonial = _context.Testimonials.Find(id);
            if(testimonial == null)
            {
                return NotFound();
            }
            var path = Helper.GetPath(_env.WebRootPath,"images", testimonial.Url);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Create (GET method)
        public IActionResult Create()
        {
            return View();
        }
        //Create (POST method)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonials testimonial)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!testimonial.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Photo size must be less than 200 KB");
                return View();
            }
            if (!testimonial.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Photo type must be image");
                return View();
            }
            testimonial.Url = await testimonial.Photo.SaveFileAsync(_env.WebRootPath, "images");
            await _context.Testimonials.AddAsync(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        //Update (GET method)
        public IActionResult Update(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Testimonials testimonials = _context.Testimonials.Find(id);
            if(testimonials == null)
            {
                return NotFound();
            }
            return View(testimonials);
        }

        //Update (POST method)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Testimonials testimonials)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Testimonials testimonialsDb = _context.Testimonials.Find(id);
            if(testimonialsDb == null)
            {
                return NotFound();
            }
            testimonials.Url = await testimonials.Photo.SaveFileAsync(_env.WebRootPath, "images");
            var pathDb = Helper.GetPath(_env.WebRootPath, "images", testimonialsDb.Url);
            if (System.IO.File.Exists(pathDb))
            {
                System.IO.File.Delete(pathDb);
            }
            testimonialsDb.Url = testimonials.Url;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
