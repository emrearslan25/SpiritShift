using SQLite;

public class Kural
{
    public int id { get; set; }
    public string kriter { get; set; }
    public int puan { get; set; } // 🔥 asıl veri
    public bool PozitifMi => puan > 0; // 🔄 dönüşüm properti
}

