using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DepoStokProgrami.Entities
{
    [Table("UrunKategori")]
    public class UrunKategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Boş Bırakılamaz"), StringLength(60, ErrorMessage = "En Fazla 60 Karakter Yazılabilir"),DisplayName("Kategori Adı")]
        public string Adi { get; set; }


        

        public virtual List<Urun>? Urunler { get; set; }

    }
}
