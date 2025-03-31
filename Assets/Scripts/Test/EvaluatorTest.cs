using UnityEngine;
using System;

public class EvaluatorTest : MonoBehaviour
{
    void Start()
    {
        // 1. Veritabanı bağlantısı
        DatabaseService db = new DatabaseService("spiritshift.db");

        // 2. Örnek kural ekleyelim (bugünün tarihiyle)
        Kural testKural = new Kural()
        {
            kriter = "hayvan",
            pozitif = true,
            tarih = DateTime.Now.ToString("yyyy-MM-dd")
        };

        db.KuralEkle(testKural);

        // 3. Kuralları yükle
        Evaluator.KurallariYukle(db);

        // 4. Test eylemi ve oyuncu kararı
        string eylem = "Sokak hayvanlarını besledi";
        bool oyuncununKarari = true; // oyuncu olumlu dedi

        // 5. Sistem değerlendirmesi
        bool dogruMu = Evaluator.KararDogruMu(eylem, oyuncununKarari);

        Debug.Log($"[TEST] Eylem: {eylem}");
        Debug.Log($"[TEST] Oyuncunun Kararı: {(oyuncununKarari ? "Olumlu" : "Olumsuz")}");
        Debug.Log($"[TEST] Karar doğru mu? {dogruMu}");
    }
}
