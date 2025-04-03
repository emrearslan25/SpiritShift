using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuhYoneticisi : MonoBehaviour
{
    public static RuhYoneticisi Instance { get; private set; }

    public RuhUIController uiController;
    public GameOverController gameOverController;
    public DatabaseService db;

    public int oyuncuID = 1;
    public string oyuncuAdi;

    private Ruh aktifRuh;
    private Zamanlayici zamanlayici;
    private KararVerici kararVerici;

    private int ruhSayaci = 0;
    private int artArdaYanlisSayaci = 0;

    void Start()
    {
        Instance = this;

        db = new DatabaseService("spiritshift.db");

        // Oyuncu bilgilerini al
        oyuncuID = PlayerPrefs.GetInt("aktifOyuncuID");
        oyuncuAdi = PlayerPrefs.GetString("aktifOyuncuAd");

        Evaluator.KurallariYukle(db);
        zamanlayici = new Zamanlayici();
        kararVerici = new KararVerici(db, oyuncuID);

        uiController.PerformansiGoster();
        YeniRuhYukle();
    }

    public void YeniRuhYukle()
    {
        string zorluk = "kolay";
        if (ruhSayaci >= 20) zorluk = "zor";
        else if (ruhSayaci >= 10) zorluk = "orta";

        aktifRuh = RuhUretici.Uret(zorluk, UnityEngine.Random.Range(5, 6));

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

        // Doğruluk analizi
        int pozitifSayisi = 0;
        foreach (var eylem in aktifRuh.eylemler)
        {
            if (Evaluator.EylemPozitifMi(eylem))
                pozitifSayisi++;
        }

        bool sistemCennetDiyor = pozitifSayisi >= 3;
        bool oyuncuDogruKararMi = (oyuncuCennetDedi == sistemCennetDiyor);

        // Performans kaydı
        Performans performans = new Performans()
        {
            oyuncu_id = oyuncuID,
            oyuncu_adi = oyuncuAdi,
            karar = oyuncuCennetDedi ? "cennet" : "cehennem",
            dogruluk = oyuncuDogruKararMi,
            sure = sure,
            tarih = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        db.PerformansKaydet(performans);

        // Oyun bitirme kontrolü
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

        // UI güncelle
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
