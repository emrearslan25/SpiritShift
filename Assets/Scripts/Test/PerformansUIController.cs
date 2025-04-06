// PerformansUIController.cs
using UnityEngine;

public class PerformansUIController : MonoBehaviour
{
    private PerformansPanelBuilder ui;

    void Start()
    {
        ui = Object.FindFirstObjectByType<PerformansPanelBuilder>();

        if (ui == null)
        {
            Debug.LogError("[PerformansUIController] UI bulunamadı! PerformansPanelBuilder sahnede mevcut mu?");
            return;
        }

        Debug.Log("[PerformansUIController] UI referansı bulundu. Veriler ayarlanıyor...");

        // Örnek veri
        string ad = "Can Gür";
        int yas = 67;
        string meslek = "Avukat";
        string olumTarihi = "08.06.2010";
        string[] eylemler = {
            "Yardım etti", "Kötü konuştu", "Sadaka verdi",
            "Kavga etti", "Dolandırdı", "Yardım etti", "İyilik yaptı"
        };

        int toplamRuh = 8;
        int dogru = 6;
        int yanlis = 2;
        float ortSure = 0.72f;
        float dogruluk = 75f;

        // Verileri UI'ya aktarma
        ui.SetRuhBilgisi(ad, yas, meslek, olumTarihi);
        ui.SetEylemler(eylemler);
        ui.SetPerformans(toplamRuh, dogru, yanlis, ortSure, dogruluk);
        ui.SetGeriBildirim("Ruhun seçimlerine göre değerlendirme yapılıyor.");
    }
}
