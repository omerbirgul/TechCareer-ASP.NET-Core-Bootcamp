using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efCore.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            this._context = context;
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Index()
        {
            return View(await _context.Kurslar.ToListAsync());
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var kurs = await _context.Kurslar
            .Include(k => k.KursKayitlari)
            .ThenInclude(o => o.Ogrenci)
            .FirstOrDefaultAsync(k => k.KursId == id);
            if (id != null && kurs != null)
            {
                return View(kurs);

            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Kurs model, int id)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }




        public async Task<IActionResult> Delete(int? id)
        {
            var kurs = await _context.Kurslar.FirstOrDefaultAsync(o => o.KursId == id);
            if (id == null)
            {
                return NotFound();
            }
            if (kurs == null)
            {
                return NotFound();
            }
            return View(kurs);
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var kurs = await _context.Kurslar.FirstOrDefaultAsync(o => o.KursId == id);
            if(kurs == null)
            {
                return NotFound();
            }
            _context.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}