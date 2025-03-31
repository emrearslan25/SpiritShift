using System;
using UnityEngine;

public class KararVerici
{
    private DatabaseService db;
    private int oyuncuID;

    public KararVerici(DatabaseService database, int aktifOyuncuID)
    {
        db = database;
        oyuncuID = aktifOyuncuID;
    }

    public void KararVer(string eylemAciklama, bool oyuncuPozitifKarar, double kararSuresi)
    {
        bool dogruMu = Evaluator.KararDogruMu(eylemAciklama, oyuncuPozitifKarar);

        Performans kayit = new Performans()
        {
            oyuncu_id = oyuncuID,
            karar = oyuncuPozitifKarar ? "cennet" : "cehennem",
            dogruluk = dogruMu,
            sure = kararSuresi,
            tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        db.PerformansKaydet(kayit);

        Debug.Log($"[KararVerici] Karar verildi: {kayit.karar} → {(dogruMu ? "Doğru" : "Yanlış")} (Süre: {kararSuresi} sn)");
    }
}
