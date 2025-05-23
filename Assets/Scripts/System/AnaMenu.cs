using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;


public class AnaMenuController : MonoBehaviour
{
    public GameObject istatistikPanel;
    public GameObject isimInput;
    public GameObject menuButonlar;
    public IstatistikPanelKontroller panelKontrol;
    public TMP_InputField isimInputField;
    public GameObject kurallarPanel;
    public KurallarPanelKontroller kurallarKontrol;
    

    public void IstatistikleriGoster()
    {
        menuButonlar.SetActive(false);
        istatistikPanel.SetActive(true);
        panelKontrol.TumIstatistikleriYukle();
        isimInput.SetActive(false); // isim inputu gizle

    }

    public void OyunaBasla()
{
    string girilenIsim = isimInputField.text.Trim();

    if (string.IsNullOrEmpty(girilenIsim))
    {
        Debug.LogWarning("İsim boş bırakılamaz.");
        return;
    }

    // Yeni oyuncu oluştur
    Oyuncu yeni = new Oyuncu()
    {
        ad = girilenIsim,
        toplam_dogru = 0,
        toplam_sure = 0,
        seviye = 1
    };

    DatabaseService db = new DatabaseService("spiritshift.db");
    db.YeniOyuncuEkle(yeni);

    // ID'yi bulalım
    var tumOyuncular = db.GetOyuncular();
    Oyuncu sonEklenen = tumOyuncular.Last(); // en son eklenen

    PlayerPrefs.SetInt("aktifOyuncuID", sonEklenen.id);
    PlayerPrefs.SetString("aktifOyuncuAd", sonEklenen.ad);

    SceneManager.LoadScene("GameScene");
}


    public void Cikis()
    {
        Application.Quit();
    }




    public void IstatistikKapat()
{
    istatistikPanel.SetActive(false);
    menuButonlar.SetActive(true);
    isimInput.SetActive(true);
    
}

public void KurallariGoster()
{
    kurallarPanel.SetActive(true);
    kurallarKontrol.KurallariYukle();
    menuButonlar.SetActive(false);
    isimInput.SetActive(false);
}

public void KurallariKapat()
{
    kurallarPanel.SetActive(false);
    menuButonlar.SetActive(true);
    isimInput.SetActive(true);
}

}
