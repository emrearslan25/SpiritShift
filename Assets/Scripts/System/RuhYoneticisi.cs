using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuhYoneticisi : MonoBehaviour
{
    public RuhUIController uiController;
    public DatabaseService db;
    public int oyuncuID = 1;

    private Ruh aktifRuh;
    private Zamanlayici zamanlayici;
    private KararVerici kararVerici;

    void Start()
{
    db = new DatabaseService("spiritshift.db");
    Evaluator.KurallariYukle(db);

    zamanlayici = new Zamanlayici();
    kararVerici = new KararVerici(db, oyuncuID);

    YeniRuhYukle();
}


    public void YeniRuhYukle()
    {
        aktifRuh = RuhUretici.Uret();

        uiController.RuhBilgileriniGoster(
            aktifRuh.ad,
            aktifRuh.yas,
            aktifRuh.meslek,
            aktifRuh.olumTarihi,
            aktifRuh.eylemler
        );

        zamanlayici.Baslat();
        uiController.AktifButonlariAyarla(true);
    }

    public void KararVerildi(bool oyuncuCennetDedi)
    {
        float sure = zamanlayici.BitirVeSüreyiAl();

        // Tüm eylemleri değerlendir
        int pozitifSayisi = 0;
        foreach (var eylem in aktifRuh.eylemler)
        {
            if (Evaluator.EylemPozitifMi(eylem))
                pozitifSayisi++;
        }

        bool sistemCennetDiyor = pozitifSayisi >= 4; // 7 eylemden en az 4 iyi ise cennet
        bool oyuncuDogruKararMi = (oyuncuCennetDedi == sistemCennetDiyor);

        // Performans kaydı
        Performans performans = new Performans()
        {
            oyuncu_id = oyuncuID,
            karar = oyuncuCennetDedi ? "cennet" : "cehennem",
            dogruluk = oyuncuDogruKararMi,
            sure = sure,
            tarih = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        db.PerformansKaydet(performans);

        // UI geri bildirim
        uiController.GeriBildirimVer(oyuncuDogruKararMi);
        uiController.AktifButonlariAyarla(false);

        // 2 saniye sonra yeni ruh
        StartCoroutine(BekleVeYenile());
    }

    private IEnumerator BekleVeYenile()
    {
        yield return new WaitForSeconds(2f);
        YeniRuhYukle();
    }

    public void CennetButonTiklandi()
{
    KararVerildi(true); // Oyuncu Cennet dedi
}

public void CehennemButonTiklandi()
{
    KararVerildi(false); // Oyuncu Cehennem dedi
}

}
