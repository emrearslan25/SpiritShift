using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KurallarPanelKontroller : MonoBehaviour
{
    public GameObject satirPrefab;
    public Transform contentParent;
    private DatabaseService db;

    void Awake()
    {
        db = new DatabaseService("spiritshift.db");
    }

    public void KurallariYukle()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        List<Kural> kurallar = db.GetTumKurallar();

        foreach (var kural in kurallar)
        {
            GameObject satir = Instantiate(satirPrefab, contentParent);
            satir.transform.GetChild(0).GetComponent<TMP_Text>().text = kural.kriter;
            satir.transform.GetChild(1).GetComponent<TMP_Text>().text = kural.anlam ?? "-";
        }
    }
}
