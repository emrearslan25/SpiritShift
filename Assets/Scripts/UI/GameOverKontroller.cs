using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;


public class GameOverController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text istatistikText;


    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void OyunuBitir()
{
    gameOverPanel.SetActive(true);
    
    int oyuncuID = RuhYoneticisi.Instance.oyuncuID;
    var performanslar = RuhYoneticisi.Instance.db.GetPerformanslar()
                          .Where(p => p.oyuncu_id == oyuncuID).ToList();

    int toplam = performanslar.Count;
    int dogru = performanslar.Count(p => p.dogruluk);
    float ortSure = performanslar.Count > 0 ? (float)performanslar.Average(p => p.sure) : 0;

    string rapor = $"🪪 Oyuncu ID: {oyuncuID}\n";
    rapor += $"🧠 Toplam Karar: {toplam}\n";
    rapor += $"✅ Doğru Karar: {dogru}\n";
    rapor += $"📉 Başarı Oranı: %{(toplam > 0 ? (dogru * 100 / toplam) : 0)}\n";
    rapor += $"⏱️ Ortalama Süre: {ortSure:F2} sn\n";

    istatistikText.text = rapor;
}


    public void YenidenBasla()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
