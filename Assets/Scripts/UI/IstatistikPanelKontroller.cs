using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class IstatistikPanelKontroller : MonoBehaviour
{
    public GameObject statRowPrefab;
    public Transform contentParent;

    private DatabaseService db;

    private void Awake()
    {
        db = new DatabaseService("spiritshift.db");
    }

    public void TumIstatistikleriYukle()
{
    foreach (Transform child in contentParent)
        Destroy(child.gameObject);

    List<Performans> tumPerformanslar = db.GetPerformanslar();
    var gruplu = tumPerformanslar.GroupBy(p => p.oyuncu_id);

    foreach (var grup in gruplu)
{
    GameObject row = Instantiate(statRowPrefab, contentParent);

    int toplam = grup.Count();
    int dogru = grup.Count(p => p.dogruluk);
    float ortSure = (float)grup.Average(p => p.sure);
    float oran = ((float)dogru / toplam) * 100f;

    string oyuncuAdi = grup.First().oyuncu_adi;

    row.transform.GetChild(0).GetComponent<TMP_Text>().text = oyuncuAdi;
    row.transform.GetChild(1).GetComponent<TMP_Text>().text = toplam.ToString();
    row.transform.GetChild(2).GetComponent<TMP_Text>().text = $"%{oran:F0}";
    row.transform.GetChild(3).GetComponent<TMP_Text>().text = $"{ortSure:F2} sn";
}

}

}
