using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PerformansPanelController : MonoBehaviour
{
    [Header("Metin Alanları")]
    public TMP_Text toplamText;
    public TMP_Text dogruText;
    public TMP_Text yanlisText;
    public TMP_Text ortSureText;
    public TMP_Text oranText;

    void Start()
    {

    if (RuhYoneticisi.Instance == null)
    {
        Debug.LogError("RuhYoneticisi.Instance henüz null 😢");
        return;
    }

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
