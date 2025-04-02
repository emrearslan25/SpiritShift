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

    public static List<string> RastgeleEylemlerUretKarisik(string zorluk, int adet)
{
    List<string> tumEylemler = new List<string>();

    if (iyiEylemler.ContainsKey(zorluk))
        tumEylemler.AddRange(iyiEylemler[zorluk]);
    if (kotuEylemler.ContainsKey(zorluk))
        tumEylemler.AddRange(kotuEylemler[zorluk]);

    List<string> secilen = new List<string>();
    for (int i = 0; i < adet && tumEylemler.Count > 0; i++)
    {
        int index = UnityEngine.Random.Range(0, tumEylemler.Count);
        secilen.Add(tumEylemler[index]);
        tumEylemler.RemoveAt(index);
    }

    return secilen;
}

}
