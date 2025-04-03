using SQLite;

public class Performans
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public int oyuncu_id { get; set; }
    public string oyuncu_adi { get; set; } // 👈 bunu ekle
    public string karar { get; set; }
    public bool dogruluk { get; set; }
    public double sure { get; set; }
    public string tarih { get; set; }
}

