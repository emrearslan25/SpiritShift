using System;
using System.Collections.Generic;
using UnityEngine;

public static class Evaluator
{
    private static List<Kural> guncelKurallar;

    /// <summary>
    /// Güncel kural listesini veritabanından çeker.
    /// Bu metod oyunun başında bir kere çağrılmalı.
    /// </summary>
    public static void KurallariYukle(DatabaseService db)
    {
        guncelKurallar = db.GetGuncelKurallar();
        Debug.Log($"[Evaluator] {guncelKurallar.Count} kural yüklendi.");
    }

    /// <summary>
    /// Verilen eylem açıklamasına göre bu eylem pozitif mi?
    /// Kurallar tabanlı otomatik analiz yapar.
    /// </summary>
    public static bool EylemPozitifMi(string eylemAciklama)
    {
        foreach (var kural in guncelKurallar)
        {
            if (eylemAciklama.ToLower().Contains(kural.kriter.ToLower()))
            {
                Debug.Log($"[Evaluator] Eylem eşleşti: '{eylemAciklama}' → Kriter: '{kural.kriter}' → Pozitif mi? {kural.pozitif}");
                return kural.pozitif;
            }
        }

        // Hiçbir kural eşleşmediyse gri alan, varsayılan olarak olumsuz kabul edebilirsin
        Debug.Log($"[Evaluator] Eylemde eşleşme bulunamadı: '{eylemAciklama}' → Varsayılan: false");
        return false;
    }

    /// <summary>
    /// Oyuncunun kararı ile sistemin değerlendirmesini karşılaştırır.
    /// </summary>
    public static bool KararDogruMu(string eylemAciklama, bool oyuncuPozitifMi)
    {
        bool sistemPozitifMi = EylemPozitifMi(eylemAciklama);
        return sistemPozitifMi == oyuncuPozitifMi;
    }
}
