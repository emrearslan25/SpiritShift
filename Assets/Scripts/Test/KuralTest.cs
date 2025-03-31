using UnityEngine;
using System;

public class KuralTest : MonoBehaviour
{
    void Start()
    {
        DatabaseService db = new DatabaseService("spiritshift.db");

        Kural k1 = new Kural()
        {
            kriter = "yardım",
            pozitif = true,
            tarih = DateTime.Now.ToString("yyyy-MM-dd")
        };

        Kural k2 = new Kural()
        {
            kriter = "rüşvet",
            pozitif = false,
            tarih = DateTime.Now.ToString("yyyy-MM-dd")
        };

        db.KuralEkle(k1);
        db.KuralEkle(k2);
    }
}
