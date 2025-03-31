using System;
using System.Collections.Generic;
using UnityEngine;

public static class Evaluator
{
    private static List<Kural> guncelKurallar = new List<Kural>();

    /// <summary>
    /// Güncel kural listesini veritabanından çeker.
    /// Bu metod oyunun başında bir kere çağrılmalı.
    /// </summary>
    public static void KurallariYukle(DatabaseService db)
    {
        guncelKurallar = db.GetGuncelKurallar();
    Debug.Log($"[Evaluator] KurallariYukle çağrıldı. Toplam: {guncelKurallar.Count} kural.");
    }

    /// <summary>
    /// Verilen eylem açıklamasına göre bu eylem pozitif mi?
    /// Kurallar tabanlı otomatik analiz yapar.
    /// </summary>
    public static bool EylemPozitifMi(string eylemAciklama)
    {
        if (guncelKurallar == null || guncelKurallar.Count == 0)
        {
            Debug.LogWarning("[Evaluator] Kurallar yüklenmemiş. Varsayılan olarak tüm eylemler negatif sayılır.");
            return false;
        }

        string eylem = eylemAciklama.ToLower();

        foreach (var kural in guncelKurallar)
        {
            string kriter = kural.kriter.ToLower();
            if (eylem.Contains(kriter))
            {
                Debug.Log($"[Evaluator] Eylem eşleşti: '{eylemAciklama}' → Kriter: '{kural.kriter}' → Pozitif mi? {kural.pozitif}");
                return kural.pozitif;
            }
        }

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
