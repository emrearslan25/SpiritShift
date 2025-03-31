using UnityEngine;

public class Zamanlayici
{
    private float baslangicZamani;

    public void Baslat()
    {
        baslangicZamani = Time.time;
        Debug.Log("[Zamanlayici] Zaman başladı.");
    }

    public float BitirVeSüreyiAl()
    {
        float gecenSure = Time.time - baslangicZamani;
        Debug.Log($"[Zamanlayici] Süre: {gecenSure} saniye");
        return gecenSure;
    }
}
