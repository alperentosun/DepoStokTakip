using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DepoStokProgrami.Entities;
using DepoStokProgrami.Identity;
using DepoStokProgrami.Services;


namespace DepoStokProgrami.Controllers
{
    public class UrunlerController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly IWebHostEnvironment _host;

        public UrunlerController(AppIdentityDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;

        }

        // GET: Urunler
        public async Task<IActionResult> Index()
        {
            var appIdentityDbContext = _context.Urunler.Include(u => u.UrunKategori);
            return View(await appIdentityDbContext.ToListAsync());
        }

        // GET: Urunler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.UrunKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: Urunler/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.UrunKategoriler, "Id", "Adi");
            return View();
        }

        // POST: Urunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UrunAdi,Fiyat,Marka,Adet,KdvOran,KdvliFiyat,ToplamFiyat,ResimDosya,KategoriId")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                if (urun.ResimDosya != null)
                {
                    // wwwroot/YuklenenResimler klasörüne resim yükleme işlemi
                    string wwwRootPath = _host.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(urun.ResimDosya.FileName);//Dosya Adını Aldık.
                    string extension = Path.GetExtension(urun.ResimDosya.FileName);//Yüklenen Resmin Uzantısını Aldık.
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "Upload/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await urun.ResimDosya.CopyToAsync(fileStream);
                    }

                    urun.Resim = fileName;
                    // wwwroot/YuklenenResimler klasörüne resim yükleme işlemi
                }

                urun.KdvliFiyat = AletCantam.KdvliFiyatHesapla(urun.Fiyat, urun.KdvOran);
                urun.ToplamFiyat = AletCantam.FiyatHesapla(urun.Adet, urun.KdvliFiyat);


                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.UrunKategoriler, "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        // GET: Urunler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }

            if (urun.Resim != null)
            {
                HttpContext.Session.SetString("Resim", urun.Resim);
            }
            ViewData["KategoriId"] = new SelectList(_context.UrunKategoriler, "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        // POST: Urunler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UrunAdi,Fiyat,Marka,Adet,KdvOran,KdvliFiyat,ToplamFiyat,ResimDosya,KategoriId")] Urun urun)
        {
            if (id != urun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // wwwroot/YuklenenResimler klasörüne resim yükleme işlemi
                    if (urun.ResimDosya != null && urun.ResimDosya.ToString() != "")
                    {
                        string wwwRootPath = _host.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(urun.ResimDosya.FileName);//Dosya Adını Aldık.
                        string extension = Path.GetExtension(urun.ResimDosya.FileName);//Yüklenen Resmin Uzantısını Aldık.
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath, "Upload/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await urun.ResimDosya.CopyToAsync(fileStream);
                        }

                        urun.Resim = fileName;

                        if (HttpContext.Session.GetString("Resim") != null)
                        {
                            //Resim Silindiyse Klasör içerisinden de sildireceğiz. Kök dizin klasöründen de silinecek.
                            var resimYolu = Path.Combine(_host.WebRootPath, "Upload", HttpContext.Session.GetString("Resim"));
                            if (System.IO.File.Exists(resimYolu))//Bu belirlenen yolda bir dosya var mı?
                            {
                                System.IO.File.Delete(resimYolu);//Buyoldaki dosyayı sil
                            }
                            //Resim Silindiyse Klasör içerisinden de sildireceğiz.

                        }


                    }
                    else if (HttpContext.Session.GetString("Resim") != null)
                    {
                        urun.Resim = HttpContext.Session.GetString("Resim");
                    }


                    // wwwroot/YuklenenResimler klasörüne resim yükleme işlemi

                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.UrunKategoriler, "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        // GET: Urunler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.UrunKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: Urunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Urunler == null)
            {
                return Problem("Entity set 'AppIdentityDbContext.Urunler'  is null.");
            }
            var urun = await _context.Urunler.FindAsync(id);
            if (urun != null)
            {
                //Resim Silindiyse Klasör içerisinden de sildireceğiz. Kök dizin klasöründen de silinecek.
                var resimYolu = Path.Combine(_host.WebRootPath, "Upload", urun.Resim);
                if (System.IO.File.Exists(resimYolu))//Bu belirlenen yolda bir dosya var mı?
                {
                    System.IO.File.Delete(resimYolu);//Buyoldaki dosyayı sil
                }
                //Resim Silindiyse Klasör içerisinden de sildireceğiz.
                _context.Urunler.Remove(urun);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
          return (_context.Urunler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
