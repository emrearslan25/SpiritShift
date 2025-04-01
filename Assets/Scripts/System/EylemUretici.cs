using System.Collections.Generic;
using UnityEngine;

public static class EylemUretici
{
    private static Dictionary<string, List<string>> iyiEylemler = new Dictionary<string, List<string>>()
    {
        {"kolay", new List<string>{
            "Kan bağışında bulundu",
            "Çevre temizliğine katıldı",
            "Kütüphaneye kitap bağışladı",
            "Yaşlılara yemek dağıttı",
            "Hayvan barınağında gönüllü çalıştı",
            "Hasta çocuğu hastaneye götürdü",
            "Gönüllü olarak okul boyadı",
            "Ağaç dikim kampanyasına katıldı",
            "Mahallede sağlık taraması yaptı",
            "Engelli komşusuna yardım etti"
        }}
    };

    private static Dictionary<string, List<string>> kotuEylemler = new Dictionary<string, List<string>>()
    {
        {"kolay", new List<string>{
            "Rüşvet aldı",
            "Sahte belge düzenledi",
            "Dolandırıcılık yaptı",
            "Sigara yasağını ihlal etti",
            "Uyuşturucu sattı",
            "Vergi kaçırdı",
            "Hırsızlık yaptı",
            "Kamu malına zarar verdi",
            "Trafikte alkollü yakalandı",
            "Sosyal medyada nefret söylemi yaydı"
        }}
    };

    public static List<string> RastgeleEylemlerUret(string zorluk, int adet)
    {
        List<string> eylemler = new List<string>();

        var iyi = new List<string>(iyiEylemler[zorluk]);
        var kotu = new List<string>(kotuEylemler[zorluk]);

        int iyiSayisi = adet / 2 + 1;
        int kotuSayisi = adet - iyiSayisi;

        for (int i = 0; i < iyiSayisi && iyi.Count > 0; i++)
        {
            int index = Random.Range(0, iyi.Count);
            eylemler.Add(iyi[index]);
            iyi.RemoveAt(index);
        }

        for (int i = 0; i < kotuSayisi && kotu.Count > 0; i++)
        {
            int index = Random.Range(0, kotu.Count);
            eylemler.Add(kotu[index]);
            kotu.RemoveAt(index);
        }

        for (int i = 0; i < eylemler.Count; i++)
        {
            int randIndex = Random.Range(0, eylemler.Count);
            var temp = eylemler[i];
            eylemler[i] = eylemler[randIndex];
            eylemler[randIndex] = temp;
        }

        return eylemler;
    }
}
