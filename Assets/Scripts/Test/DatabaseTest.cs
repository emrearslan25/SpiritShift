using UnityEngine;
using System.Collections.Generic;

public class DatabaseTest : MonoBehaviour
{
    void Start()
    {
        DatabaseService db = new DatabaseService("spiritshift.db");

        // Yeni oyuncu ekleyelim
        Oyuncu o = new Oyuncu()
        {
            ad = "Emre",
            toplam_dogru = 0,
            toplam_sure = 0,
            seviye = 1
        };

        db.YeniOyuncuEkle(o);
        Debug.Log("Yeni oyuncu eklendi!");

        // Oyuncuları yazdıralım
        List<Oyuncu> oyuncular = db.GetOyuncular();

        foreach (var oyuncu in oyuncular)
        {
            Debug.Log($"Oyuncu: {oyuncu.ad} - Seviye: {oyuncu.seviye}");
        }
    }
}
