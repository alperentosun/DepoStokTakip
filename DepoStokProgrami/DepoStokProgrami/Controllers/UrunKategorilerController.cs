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
    public class UrunKategorilerController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public UrunKategorilerController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: UrunKategoriler
        public async Task<IActionResult> Index()
        {
              return _context.UrunKategoriler != null ? 
                          View(await _context.UrunKategoriler.ToListAsync()) :
                          Problem("Entity set 'AppIdentityDbContext.UrunKategoriler'  is null.");
        }

        // GET: UrunKategoriler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UrunKategoriler == null)
            {
                return NotFound();
            }

            var urunKategori = await _context.UrunKategoriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunKategori == null)
            {
                return NotFound();
            }

            return View(urunKategori);
        }

        // GET: UrunKategoriler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UrunKategoriler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi")] UrunKategori urunKategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urunKategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urunKategori);
        }

        // GET: UrunKategoriler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UrunKategoriler == null)
            {
                return NotFound();
            }

            var urunKategori = await _context.UrunKategoriler.FindAsync(id);
            if (urunKategori == null)
            {
                return NotFound();
            }
            return View(urunKategori);
        }

        // POST: UrunKategoriler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi")] UrunKategori urunKategori)
        {
            if (id != urunKategori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urunKategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunKategoriExists(urunKategori.Id))
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
            return View(urunKategori);
        }

        // GET: UrunKategoriler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UrunKategoriler == null)
            {
                return NotFound();
            }

            var urunKategori = await _context.UrunKategoriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunKategori == null)
            {
                return NotFound();
            }

            return View(urunKategori);
        }

        // POST: UrunKategoriler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UrunKategoriler == null)
            {
                return Problem("Entity set 'AppIdentityDbContext.UrunKategoriler'  is null.");
            }
            var urunKategori = await _context.UrunKategoriler.FindAsync(id);
            if (urunKategori != null)
            {
                _context.UrunKategoriler.Remove(urunKategori);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunKategoriExists(int id)
        {
          return (_context.UrunKategoriler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
