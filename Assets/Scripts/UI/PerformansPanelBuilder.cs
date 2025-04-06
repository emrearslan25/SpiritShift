using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerformansPanelBuilder : MonoBehaviour
{
    public TextMeshProUGUI adText, yasText, meslekText, olumText;
    public TextMeshProUGUI toplamRuhText, dogruText, yanlisText, ortSureText, dogrulukText;
    public TextMeshProUGUI[] eylemTextleri = new TextMeshProUGUI[7];
    public TextMeshProUGUI geriBildirimText;

    void Start()
    {
        BuildUI();
    }

    public void SetRuhBilgisi(string ad, int yas, string meslek, string olumTarihi)
    {
        adText.text = "Ad: " + ad;
        yasText.text = "Yaş: " + yas;
        meslekText.text = "Meslek: " + meslek;
        olumText.text = "Ölüm Tarihi: " + olumTarihi;
    }

    public void SetEylemler(string[] eylemler)
    {
        for (int i = 0; i < eylemTextleri.Length; i++)
        {
            if (i < eylemler.Length)
            {
                eylemTextleri[i].text = $"Eylem {i + 1}: {eylemler[i]}";
                eylemTextleri[i].color = eylemler[i].ToLower().Contains("kötü") ? Color.red : Color.green;
            }
            else
            {
                eylemTextleri[i].text = $"Eylem {i + 1}: -";
                eylemTextleri[i].color = Color.gray;
            }
        }
    }

    public void SetPerformans(int toplamRuh, int dogru, int yanlis, float ortSure, float dogruluk)
    {
        toplamRuhText.text = "Toplam Ruh: " + toplamRuh;
        dogruText.text = "Doğru: " + dogru;
        yanlisText.text = "Yanlış: " + yanlis;
        ortSureText.text = "Ortalama Süre: " + ortSure.ToString("0.00") + " sn";
        dogrulukText.text = "Doğruluk: %" + dogruluk.ToString("0");
    }

    public void SetGeriBildirim(string mesaj)
    {
        geriBildirimText.text = mesaj;
    }

    void BuildUI()
    {
        Canvas canvas = FindOrCreateCanvas();
        Transform root = canvas.transform;

        // Arka Plan
        CreateBackground(root);

        // Ruh Paneli
        var ruhPanel = CreatePanel("RuhPanel", root, new Vector2(400, 160), new Vector2(0.2f, 0.8f));
        adText = AddText("Ad: ", ruhPanel.transform);
        yasText = AddText("Yaş: ", ruhPanel.transform);
        meslekText = AddText("Meslek: ", ruhPanel.transform);
        olumText = AddText("Ölüm Tarihi: ", ruhPanel.transform);

        // Performans Paneli
        var perfPanel = CreatePanel("PerformansPanel", root, new Vector2(400, 160), new Vector2(0.8f, 0.8f));
        toplamRuhText = AddText("Toplam Ruh: ", perfPanel.transform);
        dogruText = AddText("Doğru: ", perfPanel.transform);
        yanlisText = AddText("Yanlış: ", perfPanel.transform);
        ortSureText = AddText("Ortalama Süre: ", perfPanel.transform);
        dogrulukText = AddText("Doğruluk: ", perfPanel.transform);

        // Eylem Paneli
        var eylemPanel = CreatePanel("EylemPanel", root, new Vector2(600, 250), new Vector2(0.5f, 0.5f));
        for (int i = 0; i < 7; i++)
        {
            eylemTextleri[i] = AddText($"Eylem {i + 1}:", eylemPanel.transform);
        }

        // Karar Paneli
        var kararPanel = CreatePanel("KararPanel", root, new Vector2(400, 80), new Vector2(0.5f, 0.2f), false);
        CreateButton("Cennet", kararPanel.transform, new Vector2(-100, 0), new Color(0f, 0.8f, 0.2f));
        CreateButton("Cehennem", kararPanel.transform, new Vector2(100, 0), new Color(0.8f, 0f, 0.1f));

        // Geri Bildirim
        geriBildirimText = CreateText("", root, new Vector2(0.5f, 0.05f));
    }

    Canvas FindOrCreateCanvas()
    {
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas != null) return canvas;

        GameObject go = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var scaler = go.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        return canvas;
    }

    void CreateBackground(Transform parent)
    {
        GameObject bg = new GameObject("Background", typeof(Image));
        bg.transform.SetParent(parent, false);
        Image img = bg.GetComponent<Image>();
        img.color = new Color(0.1f, 0.1f, 0.1f);
        RectTransform rt = bg.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    GameObject CreatePanel(string name, Transform parent, Vector2 size, Vector2 anchor, bool useLayout = true)
    {
        GameObject panel = new GameObject(name, typeof(Image));
        panel.transform.SetParent(parent, false);
        panel.GetComponent<Image>().color = new Color(0.15f, 0.15f, 0.15f, 0.9f);

        var rt = panel.GetComponent<RectTransform>();
        rt.sizeDelta = size;
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;

        if (useLayout)
        {
            var layout = panel.AddComponent<VerticalLayoutGroup>();
            layout.childControlHeight = true;
            layout.childControlWidth = true;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;
            layout.spacing = 6;
            layout.padding = new RectOffset(10, 10, 10, 10);
            panel.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        return panel;
    }

    TextMeshProUGUI AddText(string text, Transform parent)
    {
        GameObject go = new GameObject("Text", typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        var tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = 20;
        tmp.alignment = TextAlignmentOptions.Left;
        tmp.color = Color.white;
        tmp.textWrappingMode = TextWrappingModes.NoWrap;
        return tmp;
    }

    TextMeshProUGUI CreateText(string content, Transform parent, Vector2 anchor)
    {
        GameObject go = new GameObject("Text", typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        var tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = content;
        tmp.fontSize = 18;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;

        var rt = tmp.GetComponent<RectTransform>();
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;

        return tmp;
    }

    void CreateButton(string label, Transform parent, Vector2 pos, Color color)
    {
        GameObject btnGO = new GameObject(label + "Button", typeof(Image), typeof(Button));
        btnGO.transform.SetParent(parent, false);

        RectTransform rt = btnGO.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(160, 60);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;

        Image img = btnGO.GetComponent<Image>();
        img.color = color;

        GameObject txtGO = new GameObject("Text", typeof(TextMeshProUGUI));
        txtGO.transform.SetParent(btnGO.transform, false);
        var tmp = txtGO.GetComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = 24;
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.textWrappingMode = TextWrappingModes.NoWrap;

        var txtRT = tmp.GetComponent<RectTransform>();
        txtRT.anchorMin = Vector2.zero;
        txtRT.anchorMax = Vector2.one;
        txtRT.offsetMin = Vector2.zero;
        txtRT.offsetMax = Vector2.zero;

        btnGO.GetComponent<Button>().onClick.AddListener(() => Debug.Log(label + " seçildi"));
    }
}
