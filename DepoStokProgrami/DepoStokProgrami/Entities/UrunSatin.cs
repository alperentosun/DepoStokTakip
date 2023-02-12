using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DepoStokProgrami.Entities
{
    [Table("UrunSatin")]
    public class UrunSatin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz"), Display(Name = "Ürün Adı")]
        public int UrunId { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz"), Display(Name = "Toplam Fiyat")]
        [ForeignKey("ToplamFiyatId")]
        public double ToplamFiyatId { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz"), Display(Name = "Adet")]
        public int AdetId { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [Display(Name = "Müşteri Adı")]
        public string MusteriAdi { get; set; }
        [Display(Name = " Not")]
        public string? UrunNotu { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz"), Display(Name = "Tarih"), DataType(DataType.Date)]
        public DateTime Tarih { get; set; }
        [ForeignKey("UrunId")]
        public virtual Urun? Urun { get; set; }







    }
}
