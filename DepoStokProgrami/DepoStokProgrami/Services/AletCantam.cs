namespace DepoStokProgrami.Services
{
    public class AletCantam
    {
        public static double KdvliFiyatHesapla(double fiyat, double kdvOrani)
        {
            double kdvliFiyat = ((fiyat * kdvOrani) / 100) + fiyat;
            return kdvliFiyat;
        }

        public static double FiyatHesapla(int adet, double kdvliFiyat)
        {
            double genelToplamFiyat = kdvliFiyat * adet;
            return genelToplamFiyat;
        }
    }
}
