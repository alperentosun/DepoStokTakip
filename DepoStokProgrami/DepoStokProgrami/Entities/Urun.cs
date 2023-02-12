using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.ComponentModel;

namespace DepoStokProgrami.Entities
{
    [Table("Urun")]
    public class Urun
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,DisplayName("Ürün Adı")]
        public string UrunAdi { get; set; }
        [Required(ErrorMessage ="Bu alan boş bırakılamaz"),Display(Name ="Birim Fiyat")]
        public double Fiyat { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string Marka { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public int Adet { get; set; }
        [Required,Display(Name = "KDV Oranı")]
        public double KdvOran { get; set; }
        [Display(Name = "KDV'li Fiyat")]
        public double KdvliFiyat { get; set; }
        [Display(Name = "Toplam Fiyat")]
        public double ToplamFiyat { get; set; }
        [Display(Name = "Resim")]
        public string? Resim { get; set; }
        [NotMapped, Display(Name = "Resim Seçin")]
        public IFormFile? ResimDosya { get; set; }
        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }
        [ForeignKey("KategoriId"),Display(Name ="Kategori")]
        public virtual UrunKategori? UrunKategori { get; set; }
        public virtual List<UrunSatis>? UrunSatislar { get; set; }
        public virtual List<UrunSatin>? UrunSatinAl { get; set; }


    }
}
