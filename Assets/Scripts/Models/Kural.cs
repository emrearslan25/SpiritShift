using SQLite;

public class Kural
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public string kriter { get; set; }

    public int puan { get; set; } // 1 = iyi, 0 = kötü

    public string anahtar_kelime { get; set; } // örn: "alkollü"

    public string anlam { get; set; } // örn: "Alkol kullanmak"

    public bool PozitifMi => puan > 0;
}
