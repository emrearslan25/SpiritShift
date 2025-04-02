using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuhYoneticisi : MonoBehaviour
{
    public static RuhYoneticisi Instance { get; private set; } // Singleton eklendi

    public RuhUIController uiController;
    public GameOverController gameOverController;
    public int oyuncuID = 1;

    public DatabaseService db;
    private Ruh aktifRuh;
    private Zamanlayici zamanlayici;
    private KararVerici kararVerici;
    private int ruhSayaci = 0;
    private int artArdaYanlisSayaci = 0;

    void Start()
    {
        Instance = this; // Singleton ataması

        db = new DatabaseService("spiritshift.db");

        // Her oyun başında yeni oyuncu oluştur
        oyuncuID = UnityEngine.Random.Range(1000, 9999);
        db.YeniOyuncuEkle(new Oyuncu { id = oyuncuID, ad = "Oyuncu" + oyuncuID });

        Evaluator.KurallariYukle(db);

        zamanlayici = new Zamanlayici();
        kararVerici = new KararVerici(db, oyuncuID);

        uiController.PerformansiGoster(); // Başlangıçta sıfırdan göster
        YeniRuhYukle();
    }

    public void YeniRuhYukle()
    {
        string zorluk = "kolay";
        if (ruhSayaci >= 20) zorluk = "zor";
        else if (ruhSayaci >= 10) zorluk = "orta";

        aktifRuh = RuhUretici.Uret(zorluk, UnityEngine.Random.Range(5, 6)); // rastgele sayıda eylem (5)

        uiController.RuhBilgileriniGoster(
            aktifRuh.ad,
            aktifRuh.yas,
            aktifRuh.meslek,
            aktifRuh.olumTarihi,
            aktifRuh.eylemler
        );

        zamanlayici.Baslat();
        uiController.AktifButonlariAyarla(true);
        uiController.PerformansiGoster();
    }

    public void KararVerildi(bool oyuncuCennetDedi)
    {
        float sure = zamanlayici.BitirVeSüreyiAl();

        int pozitifSayisi = 0;
        foreach (var eylem in aktifRuh.eylemler)
        {
            if (Evaluator.EylemPozitifMi(eylem))
                pozitifSayisi++;
        }

        bool sistemCennetDiyor = pozitifSayisi >= 3; // 5 eylem → 3 iyiyse cennet
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

        if (!oyuncuDogruKararMi)
        {
            artArdaYanlisSayaci++;
            if (artArdaYanlisSayaci >= 2)
            {
                Debug.LogError("[OYUN] Art arda 2 yanlış karar verildi. OYUN BİTTİ.");
                gameOverController.OyunuBitir();
                return;
            }
        }
        else
        {
            artArdaYanlisSayaci = 0;
        }

        uiController.GeriBildirimVer(oyuncuDogruKararMi);
        uiController.AktifButonlariAyarla(false);
        uiController.PerformansiGoster();

        ruhSayaci++;
        StartCoroutine(BekleVeYenile());
    }

    private IEnumerator BekleVeYenile()
    {
        yield return new WaitForSeconds(2f);
        YeniRuhYukle();
    }

    public void CennetButonTiklandi()
    {
        KararVerildi(true);
    }

    public void CehennemButonTiklandi()
    {
        KararVerildi(false);
    }
}