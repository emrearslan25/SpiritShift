using UnityEngine;

public class ActionPanelManager : MonoBehaviour
{
    public GameObject eylemPaneli;

    public void EylemleriAc()
    {
        eylemPaneli.SetActive(true);
    }

    public void GeriKapat()
    {
        eylemPaneli.SetActive(false);
    }
}
