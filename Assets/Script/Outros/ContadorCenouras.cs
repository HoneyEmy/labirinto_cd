using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContadorCenouras : MonoBehaviour
{
    [Header("Contagem")]
    public int totalCenouras = 10;

    [Header("Aparência")]
    public float tamanhoFonte = 60f;
    public Color corTexto = Color.white;
    public Vector2 posicao = new Vector2(150f, -70f);

    private TextMeshProUGUI texto;
    private int ultimoValor = -1;

    void Start()
    {
        CriarInterface();
        AtualizarTexto();
    }

    void CriarInterface()
    {
        GameObject canvasObj = new GameObject("CanvasContador");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        GameObject textoObj = new GameObject("TextoContador", typeof(RectTransform));
        textoObj.transform.SetParent(canvasObj.transform, false);

        RectTransform rt = textoObj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0f, 1f);
        rt.anchorMax = new Vector2(0f, 1f);
        rt.pivot = new Vector2(0f, 1f);
        rt.sizeDelta = new Vector2(400f, 100f);
        rt.anchoredPosition = posicao;

        texto = textoObj.AddComponent<TextMeshProUGUI>();
        texto.fontSize = tamanhoFonte;
        texto.color = corTexto;
        texto.fontStyle = FontStyles.Bold;
        texto.alignment = TextAlignmentOptions.Left;
        texto.raycastTarget = false;
        texto.outlineWidth = 0.25f;
        texto.outlineColor = Color.black;
    }

    void Update()
    {
        if (MovimentoPersonagem.quantidadeColetada != ultimoValor)
            AtualizarTexto();
    }

    void AtualizarTexto()
    {
        ultimoValor = MovimentoPersonagem.quantidadeColetada;

        if (texto != null)
            texto.text = ultimoValor + "/" + totalCenouras;
    }
}