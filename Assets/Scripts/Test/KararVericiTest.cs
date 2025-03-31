using UnityEngine;

public class KararVericiTest : MonoBehaviour
{
    void Start()
    {
        // 1. Veritabanını bağla
        DatabaseService db = new DatabaseService("spiritshift.db");

        // 2. Kuralları yükle
        Evaluator.KurallariYukle(db);

        // 3. Karar verici sistemini oluştur
        KararVerici kararVerici = new KararVerici(db, 1); // oyuncu ID = 1

        // 4. Test eylemini simüle et
        string testEylem = "Sokak hayvanlarını besledi"; // Açıklama
        bool oyuncununKarari = true; // Oyuncu 'iyi' dedi
        double kararSuresi = 4.5; // saniye

        // 5. Karar verdir ve sonucu veritabanına yaz
        kararVerici.KararVer(testEylem, oyuncununKarari, kararSuresi);
    }
}
