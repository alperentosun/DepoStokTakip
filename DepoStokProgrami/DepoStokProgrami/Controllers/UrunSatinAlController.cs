using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepoStokProgrami.Entities;
using DepoStokProgrami.Identity;

namespace DepoStokProgrami.Controllers
{
    public class UrunSatinAlController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public UrunSatinAlController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: UrunSatinAl
        public async Task<IActionResult> Index()
        {
            var appIdentityDbContext = _context.UrunSatinAl.Include(u => u.Urun);
            return View(await appIdentityDbContext.ToListAsync());
        }

        // GET: UrunSatinAl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UrunSatinAl == null)
            {
                return NotFound();
            }

            var urunSatin = await _context.UrunSatinAl
                .Include(u => u.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunSatin == null)
            {
                return NotFound();
            }

            return View(urunSatin);
        }

        // GET: UrunSatinAl/Create
        public IActionResult Create()
        {
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "UrunAdi");
            return View();
        }

        // POST: UrunSatinAl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UrunId,ToplamFiyatId,AdetId,MusteriAdi,UrunNotu,Tarih")] UrunSatin urunSatin)
        {
            if (ModelState.IsValid)
            {
                var urun = _context.Urunler.Find(urunSatin.UrunId);
                if (urun.Adet <= urunSatin.AdetId)
                {
                    urun.Adet += urunSatin.AdetId;




                    _context.Add(urunSatin);
                    _context.Update(urun);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(urunSatin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Marka", urunSatin.UrunId);
            return View(urunSatin);
        }

        // GET: UrunSatinAl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UrunSatinAl == null)
            {
                return NotFound();
            }

            var urunSatin = await _context.UrunSatinAl.FindAsync(id);
            if (urunSatin == null)
            {
                return NotFound();
            }
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Marka", urunSatin.UrunId);
            return View(urunSatin);
        }

        // POST: UrunSatinAl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UrunId,ToplamFiyatId,AdetId,MusteriAdi,UrunNotu,Tarih")] UrunSatin urunSatin)
        {
            if (id != urunSatin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urunSatin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunSatinExists(urunSatin.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Marka", urunSatin.UrunId);
            return View(urunSatin);
        }

        // GET: UrunSatinAl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UrunSatinAl == null)
            {
                return NotFound();
            }

            var urunSatin = await _context.UrunSatinAl
                .Include(u => u.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunSatin == null)
            {
                return NotFound();
            }

            return View(urunSatin);
        }

        // POST: UrunSatinAl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UrunSatinAl == null)
            {
                return Problem("Entity set 'AppIdentityDbContext.UrunSatinAl'  is null.");
            }
            var urunSatin = await _context.UrunSatinAl.FindAsync(id);
            if (urunSatin != null)
            {
                _context.UrunSatinAl.Remove(urunSatin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunSatinExists(int id)
        {
          return (_context.UrunSatinAl?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
