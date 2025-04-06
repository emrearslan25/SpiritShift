using System;
using System.Collections.Generic;
using System.Linq; // <-- LINQ unutulmasın
using UnityEngine;

public static class Evaluator
{
    private static List<Kural> guncelKurallar;
    private static Queue<bool> sonBesKarar = new Queue<bool>(); // 5'lik pencere

    public static void KurallariYukle(DatabaseService db)
    {
        guncelKurallar = db.GetGuncelKurallar();
        Debug.Log($"[Evaluator] {guncelKurallar.Count} kural yüklendi.");
    }

    public static bool EylemPozitifMi(string eylem)
    {
        var kural = guncelKurallar?.FirstOrDefault(k => k.kriter == eylem);
        if (kural == null)
        {
            Debug.LogWarning("[Evaluator] Kurallar yüklenmemiş veya eşleşme yok. Varsayılan olarak tüm eylemler negatif sayılır.");
            return false;
        }

        Debug.Log($"[Evaluator] Eylem eşleşti: '{eylem}' → Kriter: '{kural.kriter}' → Pozitif mi? {kural.PozitifMi}");
        return kural.PozitifMi;
    }

    public static bool KararDogruMu(string eylemAciklama, bool oyuncuPozitifMi)
    {
        bool sistemPozitifMi = EylemPozitifMi(eylemAciklama);
        return sistemPozitifMi == oyuncuPozitifMi;
    }

    public static void SonKarariEkle(bool dogruMu)
    {
        if (sonBesKarar.Count >= 5)
            sonBesKarar.Dequeue();

        sonBesKarar.Enqueue(dogruMu);
    }

    public static bool OyuncuBasarisizMi()
    {
        if (sonBesKarar.Count < 5)
            return false; // henüz yeterli veri yok

        int dogruSayisi = 0;
        foreach (var karar in sonBesKarar)
        {
            if (karar) dogruSayisi++;
        }

        float oran = dogruSayisi / 5f;
        return oran < 0.4f; // %40 altıysa başarısız say
    }
}
