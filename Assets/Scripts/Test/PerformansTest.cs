using UnityEngine;
using System;

public class PerformansTest : MonoBehaviour
{
    void Start()
    {
        DatabaseService db = new DatabaseService("spiritshift.db");

        Performans p = new Performans()
        {
            oyuncu_id = 1, // oyuncu daha önce eklendiyse ID 1 olabilir
            karar = "cennet",
            dogruluk = true,
            sure = 4.75,
            tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        db.PerformansKaydet(p);
    }
}
