using System;
using System.Collections.Generic;
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

    public static bool EylemPozitifMi(string eylemAciklama)
    {
        if (guncelKurallar == null || guncelKurallar.Count == 0)
        {
            Debug.LogWarning("[Evaluator] Kurallar yüklenmemiş. Varsayılan olarak tüm eylemler negatif sayılır.");
            return false;
        }

        foreach (var kural in guncelKurallar)
        {
            if (eylemAciklama.ToLower().Contains(kural.kriter.ToLower()))
            {
                Debug.Log($"[Evaluator] Eylem eşleşti: '{eylemAciklama}' → Kriter: '{kural.kriter}' → Pozitif mi? {kural.pozitif}");
                return kural.pozitif;
            }
        }

        Debug.Log($"[Evaluator] Eylemde eşleşme bulunamadı: '{eylemAciklama}' → Varsayılan: false");
        return false;
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
