using SQLite;

public class Kural
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public string kriter { get; set; } // anahtar kelime: örn. "yardım", "rüşvet"
    public bool pozitif { get; set; }  // olumlu mu olumsuz mu
    public string tarih { get; set; }  // bu kural hangi tarihte geçerli
}
