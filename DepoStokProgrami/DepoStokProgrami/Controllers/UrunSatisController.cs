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
    public class UrunSatisController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public UrunSatisController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: UrunSatis
        public async Task<IActionResult> Index(Urun urun)
        {
            
            
            var appIdentityDbContext = _context.UrunSatislar.Include(u => u.Urun);
            return View(await appIdentityDbContext.ToListAsync());

        }

        // GET: UrunSatis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UrunSatislar == null)
            {
                return NotFound();
            }

            var urunSatis = await _context.UrunSatislar
                .Include(u => u.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunSatis == null)
            {
                return NotFound();
            }

            return View(urunSatis);
        }

        // GET: UrunSatis/Create
        public IActionResult Create()
        {
            

            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "UrunAdi");
            

            return View();
        }

        // POST: UrunSatis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UrunId,ToplamFiyatId,AdetId,MusteriAdi,UrunNotu,Tarih")] UrunSatis urunSatis)
        {
            if (ModelState.IsValid)
            {
                

                var urun = _context.Urunler.Find(urunSatis.UrunId);
                

                if (urun.Adet >= urunSatis.AdetId)
                {   
                    urun.Adet -= urunSatis.AdetId;

                    


                    _context.Add(urunSatis);
                    _context.Update(urun);
                    
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    
                    ViewBag.ErrorMessage = "STOKTA MEVCUT DEĞİL!!";
                    return View();
                }


               
            }
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Marka", urunSatis.UrunId);
            return View(urunSatis);
        }

        // GET: UrunSatis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UrunSatislar == null)
            {
                return NotFound();
            }

            var urunSatis = await _context.UrunSatislar.FindAsync(id);
            if (urunSatis == null)
            {
                return NotFound();
            }
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Marka", urunSatis.UrunId);
            return View(urunSatis);
        }

        // POST: UrunSatis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UrunId,ToplamFiyatId,AdetId,MusteriAdi,UrunNotu,Tarih")] UrunSatis urunSatis)
        {
            if (id != urunSatis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urunSatis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunSatisExists(urunSatis.Id))
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
            
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Marka", urunSatis.UrunId);
            return View(urunSatis);
        }

        // GET: UrunSatis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UrunSatislar == null)
            {
                return NotFound();
            }

            var urunSatis = await _context.UrunSatislar
                .Include(u => u.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunSatis == null)
            {
                return NotFound();
            }

            return View(urunSatis);
        }

        // POST: UrunSatis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UrunSatislar == null)
            {
                return Problem("Entity set 'AppIdentityDbContext.UrunSatislar'  is null.");
            }
            var urunSatis = await _context.UrunSatislar.FindAsync(id);
            if (urunSatis != null)
            {
                _context.UrunSatislar.Remove(urunSatis);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunSatisExists(int id)
        {
          return (_context.UrunSatislar?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
