using System.Collections.Generic;
using UnityEngine;

public static class EylemUretici
{
    // Tüm olası eylem havuzu (etiketsiz, sadece açıklama)
    private static List<string> eylemHavuzu = new List<string>()
    {
        // Pozitif eylemler
        "Sokak hayvanlarını düzenli olarak besledi",
        "Kan bağışı kampanyasına katıldı ve teşvik etti",
        "Çocuklara kitap ve kırtasiye yardımında bulundu",
        "Mahallede çevre temizliği organize etti",
        "Yaşlı komşusuna market alışverişinde yardımcı oldu",
        "Online yardım kampanyası başlattı ve yönetti",
        "Doğa yürüyüşü ve temizlik etkinliği düzenledi",
        "Kütüphane kurulması için kitap bağışladı",
        "Organ bağışçısı oldu ve çevresini bilinçlendirdi",
        "Parka ağaç dikimi organizasyonuna katıldı",
        "Gönüllü temizlik kampanyasında çalıştı",
        "Bakım evinde gönüllü olarak görev yaptı",
        "Hasta çocuklara moral etkinliği düzenledi",
        "Empati atölyesinde moderatörlük yaptı",
        "Sağlık taramasında kayıt işleriyle ilgilendi",

        // Negatif eylemler
        "Vergi kaçırdı ve gelirini gizledi",
        "Sahte yatırım vaadiyle insanları dolandırdı",
        "Sahte belge hazırlayıp yalan ifade verdi",
        "İhale alabilmek için rüşvet verdi",
        "Alkollü araç kullanarak kazaya sebep oldu",
        "İnsanlara bağırarak sözlü şiddet uyguladı",
        "Komşusunu yaralayıp tehdit etti",
        "Market ürünlerini çalıp yakalandı",
        "Sigara yasağını ihlal etti",
        "Uyuşturucu kullandı ve yaydı",
        "Panik yaratmak için sahte haber paylaştı",
        "Çalışanlara küfür ederek ortamı bozdu",
        "Öğrencilere zorbalık yaptı",
        "Sosyal medyada hakaret etti",
        "Ailesine fiziksel şiddet uyguladı"
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
