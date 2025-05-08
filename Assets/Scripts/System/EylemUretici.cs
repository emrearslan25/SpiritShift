using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EylemUretici
{
    public static List<string> RastgeleEylemlerUretKarisik(string zorluk, int adet)
    {
        // 1. Veritabanı bağlantısı
        DatabaseService db = new DatabaseService("spiritshift.db");
        List<Kural> tumKurallar = db.GetTumKurallar();

        // 2. Karışık şekilde eylemleri seç
        List<string> secilen = tumKurallar
            .OrderBy(k => Random.value)
            .Take(adet)
            .Select(k => k.kriter)
            .ToList();

        return secilen;
    }

    public static List<string> RastgeleEylemlerUretFiltreli(string zorluk, int adet, int pozitifSayisi)
    {
        DatabaseService db = new DatabaseService("spiritshift.db");
        List<Kural> pozitif = db.GetTumKurallar().Where(k => k.puan > 0).OrderBy(k => Random.value).Take(pozitifSayisi).ToList();
        List<Kural> negatif = db.GetTumKurallar().Where(k => k.puan == 0).OrderBy(k => Random.value).Take(adet - pozitifSayisi).ToList();

        var birlesik = pozitif.Concat(negatif).OrderBy(k => Random.value).Select(k => k.kriter).ToList();
        return birlesik;
    }
}
