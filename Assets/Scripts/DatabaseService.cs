using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SQLite;

public class DatabaseService
{
    private SQLiteConnection _connection;

    public DatabaseService(string dbName)
    {
        string dbPath = GetDatabasePath(dbName);
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Veritabanına bağlanıldı: " + dbPath);
    }

    private string GetDatabasePath(string dbName)
    {
        string persistentPath = Path.Combine(Application.persistentDataPath, dbName);

        if (!File.Exists(persistentPath))
        {
            Debug.Log("Veritabanı streamingAssets'ten kopyalanıyor...");
#if UNITY_ANDROID && !UNITY_EDITOR
            string sourcePath = Path.Combine(Application.streamingAssetsPath, dbName);
            WWW reader = new WWW(sourcePath);
            while (!reader.isDone) { }
            File.WriteAllBytes(persistentPath, reader.bytes);
#else
            string sourcePath = Path.Combine(Application.streamingAssetsPath, dbName);
            File.Copy(sourcePath, persistentPath);
#endif
        }

        return persistentPath;
    }

    public List<Oyuncu> GetOyuncular()
    {
        return _connection.Table<Oyuncu>().ToList();
    }

    public void YeniOyuncuEkle(Oyuncu o)
    {
        _connection.Insert(o);
    }

    public void PerformansKaydet(Performans p)
    {
    _connection.Insert(p);
    Debug.Log("Performans kaydedildi.");
    }

    public List<Performans> GetPerformanslar()
    {
        return _connection.Table<Performans>().ToList();
    }

    public void KuralEkle(Kural kural)
    {
        _connection.Insert(kural);
        Debug.Log("Kural eklendi: " + kural.kriter);
    }

    public List<Kural> GetGuncelKurallar()
    {
        string bugun = System.DateTime.Now.ToString("yyyy-MM-dd");
        return _connection.Table<Kural>().Where(k => k.tarih == bugun).ToList();
    }


}
