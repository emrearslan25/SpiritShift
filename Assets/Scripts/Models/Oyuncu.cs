using SQLite;

public class Oyuncu
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }

    public string ad { get; set; }
    public int toplam_dogru { get; set; }
    public double toplam_sure { get; set; }
    public int seviye { get; set; }
}
