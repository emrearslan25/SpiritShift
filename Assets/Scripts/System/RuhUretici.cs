using System;
using System.Collections.Generic;
using UnityEngine;

public class Ruh
{
    public string ad;
    public int yas;
    public string meslek;
    public string olumTarihi;
    public List<string> eylemler;
    public string zorluk; // yeni eklendi
}

public static class RuhUretici
{
    private static List<string> isimler = new List<string>()
    {
        "Ali Demir", "Ayşe Kara", "Mehmet Yılmaz", "Elif Aydın",
        "Fatma Öz", "Can Gür", "Zeynep Polat", "Emre Koç",
        "Hatice Yıldız", "Ahmet Şahin"
    };

    private static List<string> meslekler = new List<string>()
    {
        "Öğretmen", "Mühendis", "Avukat", "Doktor",
        "Çiftçi", "Yazılımcı", "Tasarımcı", "Garson",
        "Polis", "Hemşire"
    };

    public static Ruh Uret(string zorluk, int adet)
    {
        Ruh yeniRuh = new Ruh();
        yeniRuh.ad = isimler[UnityEngine.Random.Range(0, isimler.Count)];
        yeniRuh.yas = UnityEngine.Random.Range(25, 85);
        yeniRuh.meslek = meslekler[UnityEngine.Random.Range(0, meslekler.Count)];
        yeniRuh.olumTarihi = RastgeleTarih();
        yeniRuh.eylemler = EylemUretici.RastgeleEylemlerUretKarisik(zorluk, adet);
        return yeniRuh;
    }

    private static string RastgeleTarih()
    {
        int yil = UnityEngine.Random.Range(2000, 2025);
        int ay = UnityEngine.Random.Range(1, 13);
        int gun = UnityEngine.Random.Range(1, 29);
        return $"{gun:D2}.{ay:D2}.{yil}";
    }
}