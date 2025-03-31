using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;


public class RuhUIController : MonoBehaviour
{
    // UI Referansları (Inspector üzerinden bağlanacak)
    [Header("Ruh Paneli")]
    public TMP_Text adText;
    public TMP_Text yasText;
    public TMP_Text meslekText;
    public TMP_Text olumTarihiText;
    public Button cennetButton;
    public Button cehennemButton;


    [Header("Eylem Paneli")]
    public List<TMP_Text> eylemTextList; // 7 adet text

    [Header("Karar Paneli")]
    public TMP_Text geriBildirimText;

    /// <summary>
    /// UI'yı verilen ruh bilgileriyle doldurur
    /// </summary>
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
                eylemTextList[i].text = ""; // fazladan varsa temizle
        }

        geriBildirimText.text = ""; // önceki karar mesajını sıfırla
    }

    /// <summary>
    /// Karar sonrası geri bildirim verir
    /// </summary>
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

}
