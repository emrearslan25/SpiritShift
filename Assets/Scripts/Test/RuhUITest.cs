using UnityEngine;
using System.Collections.Generic;

public class RuhUI_Test : MonoBehaviour
{
    public RuhUIController uiController;

    void Start()
    {
        List<string> sahteEylemler = new List<string>()
        {
            "Sokak hayvanlarını besledi",
            "Vergi kaçırdı",
            "Yaşlılara yardım etti",
            "Hırsızlık yaptı",
            "Kan bağışladı",
            "Yalan söyledi",
            "Çevre temizliği yaptı"
        };

        uiController.RuhBilgileriniGoster("Ali Demir", 68, "Emekli Öğretmen", "12.03.2023", sahteEylemler);

        // 2 saniye sonra karar simülasyonu
        Invoke("KararSonucuGoster", 2f);
    }

    void KararSonucuGoster()
    {
        // True → doğru karar simülasyonu
        uiController.GeriBildirimVer(true);
    }
}
