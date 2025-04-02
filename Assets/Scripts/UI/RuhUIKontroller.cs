using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class RuhUIController : MonoBehaviour
{
    [Header("Ruh Paneli")]
    public TMP_Text adText;
    public TMP_Text yasText;
    public TMP_Text meslekText;
    public TMP_Text olumTarihiText;
    public Button cennetButton;
    public Button cehennemButton;
    public Button acKapa;

    [Header("Eylem Paneli")]
    public List<TMP_Text> eylemTextList; // 7 adet text

    [Header("Karar Paneli")]
    public TMP_Text geriBildirimText;

    [Header("Performans Paneli")]
    public TMP_Text toplamText;
    public TMP_Text dogruText;
    public TMP_Text yanlisText;
    public TMP_Text ortSureText;
    public TMP_Text oranText;

    public void RuhBilgileriniGoster(string ad, int yas, string meslek, string olumTarihi, List<string> eylemler)
    {
        adText.text = "Ad: " + ad;
        yasText.text = "Yaş: " + yas.ToString();
        meslekText.text = "Meslek: " + meslek;
        olumTarihiText.text = "Ölüm Tarihi: " + olumTarihi;

        for (int i = 0; i < eylemTextList.Count; i++)
        {
            if (i < eylemler.Count)
                eylemTextList[i].text = eylemler[i];
            else
                eylemTextList[i].text = "";
        }

        geriBildirimText.text = "";
    }

    public void GeriBildirimVer(bool dogruMu)
    {
        geriBildirimText.text = dogruMu ? "✅ Doğru karar!" : "❌ Yanlış karar!";
        geriBildirimText.color = dogruMu ? Color.green : Color.red;
    }

    public void AktifButonlariAyarla(bool aktif)
    {
        cennetButton.interactable = aktif;
        cehennemButton.interactable = aktif;
    }

    public void PerformansiGoster()
    {
        var db = RuhYoneticisi.Instance.db;
        var oyuncuID = RuhYoneticisi.Instance.oyuncuID;
        List<Performans> kayitlar = db.GetPerformanslar().Where(p => p.oyuncu_id == oyuncuID).ToList();

        int toplam = kayitlar.Count;
        int dogru = kayitlar.Count(p => p.dogruluk);
        int yanlis = toplam - dogru;
        double ortSure = toplam > 0 ? kayitlar.Average(p => p.sure) : 0;
        double oran = toplam > 0 ? (double)dogru / toplam * 100f : 0;

        toplamText.text = "Toplam Ruh: " + toplam;
        dogruText.text = "Doğru: " + dogru;
        yanlisText.text = "Yanlış: " + yanlis;
        ortSureText.text = $"Ortalama Süre: {ortSure:F2} sn";
        oranText.text = $"Doğruluk: %{oran:F1}";
    }
}
