
using DepoStokProgrami.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DepoStokProgrami.Identity
{
    public class AppIdentityDbContext: IdentityDbContext<AppIdentityUser,AppIdentityRole,string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext>options):base(options) 
        {

        }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<UrunKategori> UrunKategoriler { get; set; }
        public DbSet<UrunSatis> UrunSatislar { get; set; }
        public DbSet<UrunSatin> UrunSatinAl { get; set; }


    }
}
