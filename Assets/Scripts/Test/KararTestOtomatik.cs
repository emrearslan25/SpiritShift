using UnityEngine;

public class KararTestOtomatik : MonoBehaviour
{
    DatabaseService db;
    KararVerici kararVerici;
    Zamanlayici zamanlayici;

    string testEylem = "Sokak hayvanlarını besledi";
    bool oyuncununKarari = true; // Oyuncu "iyi" dedi

    void Start()
    {
        db = new DatabaseService("spiritshift.db");
        Evaluator.KurallariYukle(db);
        kararVerici = new KararVerici(db, 1);
        zamanlayici = new Zamanlayici();

        zamanlayici.Baslat(); // süre başlasın

        Invoke("KararVer", 5f); // 5 saniye sonra karar versin (simülasyon)
    }

    void KararVer()
    {
        float sure = zamanlayici.BitirVeSüreyiAl();
        kararVerici.KararVer(testEylem, oyuncununKarari, sure);
    }
}
