using SQLite;

public class Performans
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public int oyuncu_id { get; set; }
    public string karar { get; set; } // "cennet" veya "cehennem"
    public bool dogruluk { get; set; }
    public double sure { get; set; } // saniye cinsinden
    public string tarih { get; set; }
}
