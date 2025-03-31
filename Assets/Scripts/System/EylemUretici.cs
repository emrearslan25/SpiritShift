using System.Collections.Generic;
using UnityEngine;

public static class EylemUretici
{
    // Tüm olası eylem havuzu (etiketsiz, sadece açıklama)
    private static List<string> eylemHavuzu = new List<string>()
    {
        "Sokak hayvanlarını besledi",
        "Vergi kaçırdı",
        "Kan bağışında bulundu",
        "Dolandırıcılık yaptı",
        "Çocuklara kitap hediye etti",
        "Yalan söyledi",
        "Çevre temizliği yaptı",
        "Rüşvet aldı",
        "Yaşlılara yardım etti",
        "Alkollü araç kullandı",
        "Yardım kampanyası düzenledi",
        "İnsanlara bağırdı",
        "Doğa yürüyüşü organize etti",
        "Birini yaraladı",
        "Kütüphane bağışladı"
    };

    /// <summary>
    /// Rastgele 7 eylem üretir, etiketsizdir. Değerlendirmeyi sistem yapar.
    /// </summary>
    public static List<string> RastgeleEylemlerUret()
    {
        List<string> kopya = new List<string>(eylemHavuzu);
        List<string> secilen = new List<string>();

        for (int i = 0; i < 7; i++)
        {
            int rast = Random.Range(0, kopya.Count);
            secilen.Add(kopya[rast]);
            kopya.RemoveAt(rast);
        }

        return secilen;
    }
}
