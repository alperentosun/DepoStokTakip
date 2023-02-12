using DepoStokProgrami.Identity;
using DepoStokProgrami.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DepoStokProgrami.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdentityDbContext _context;

        public HomeController(AppIdentityDbContext context,ILogger<HomeController> logger)
        {
            _logger = logger;
            _context =context;
        }

        public IActionResult Index()
        {
            Dashboards raporbilgi = new Dashboards();
            raporbilgi.GelirToplam = _context.UrunSatislar.Sum(g => g.ToplamFiyatId);
            raporbilgi.GiderToplam = _context.Urunler.Sum(g => g.ToplamFiyat);
            raporbilgi.GiderToplam = _context.UrunSatinAl.Sum(g => g.ToplamFiyatId);
            raporbilgi.KategoriSayisi = _context.UrunKategoriler.Count();
            raporbilgi.KarBilgisi = raporbilgi.GelirToplam - raporbilgi.GiderToplam;
            raporbilgi.AdetSayisi = _context.Urunler.Sum(g => g.Adet);
            return View(raporbilgi);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}