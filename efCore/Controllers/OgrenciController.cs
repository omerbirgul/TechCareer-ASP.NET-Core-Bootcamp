using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace efCore.Controllers
{
    public class OgrenciController : Controller
    {

        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
            this._context = context;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogrenciler.ToListAsync());
        }
    }
}