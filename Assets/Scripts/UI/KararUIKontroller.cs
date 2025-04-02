using UnityEngine;
using UnityEngine.UI;

public class KararUIController : MonoBehaviour
{
    public Button cennetButton;
    public Button cehennemButton;

    private DatabaseService db;
    private KararVerici kararVerici;
    private Zamanlayici zamanlayici;

    private string aktifEylem = "Sokak hayvanlarını besledi"; // örnek

    void Start()
    {
        db = new DatabaseService("spiritshift.db");
        Evaluator.KurallariYukle(db);
        kararVerici = new KararVerici(db, 1);
        zamanlayici = new Zamanlayici();

        zamanlayici.Baslat();

        cennetButton.onClick.AddListener(() => OyuncuKararVerdi(true));
        cehennemButton.onClick.AddListener(() => OyuncuKararVerdi(false));
    }

    void OyuncuKararVerdi(bool oyuncuPozitifKarar)
    {
        float sure = zamanlayici.BitirVeSüreyiAl();
        kararVerici.KararVer(aktifEylem, oyuncuPozitifKarar, sure);

        // Butonları devre dışı bırak, yeni eylem gelene kadar
        cennetButton.interactable = false;
        cehennemButton.interactable = false;
    }

    
}
