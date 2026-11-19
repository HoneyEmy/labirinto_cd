using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPausa : MonoBehaviour
{
    [Header("Configuração")]
    public KeyCode teclaPausa = KeyCode.Escape;
    public string nomeCenaMenu = "MenuInicial";   // nome EXATO da cena do menu

    private GameObject painel;
    private bool pausado = false;

    void Start()
    {
        Time.timeScale = 1f;
        CriarInterface();
        painel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaPausa))
        {
            if (pausado) Continuar();
            else Pausar();
        }
    }

    void Pausar()
    {
        pausado = true;
        painel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continuar()
    {
        pausado = false;
        painel.SetActive(false);
        Time.timeScale = 1f;
    }

    void IrParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nomeCenaMenu);
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    // ---------- Interface criada por código ----------

    void CriarInterface()
    {
        GameObject canvasObj = new GameObject("CanvasPausa");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 2000;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasObj.AddComponent<GraphicRaycaster>();

        // Fundo escuro
        painel = new GameObject("PainelPausa", typeof(RectTransform));
        painel.transform.SetParent(canvasObj.transform, false);
        RectTransform rtPainel = painel.GetComponent<RectTransform>();
        rtPainel.anchorMin = Vector2.zero;
        rtPainel.anchorMax = Vector2.one;
        rtPainel.offsetMin = Vector2.zero;
        rtPainel.offsetMax = Vector2.zero;
        Image fundo = painel.AddComponent<Image>();
        fundo.color = new Color(0f, 0f, 0f, 0.7f);

        CriarTexto(painel.transform, "PAUSADO", 90f, new Vector2(0f, 200f));
        CriarBotao(painel.transform, "Continuar", new Vector2(0f, 20f), Continuar);
        CriarBotao(painel.transform, "Menu Inicial", new Vector2(0f, -140f), IrParaMenu);
    }

    void CriarTexto(Transform pai, string conteudo, float tamanho, Vector2 pos)
    {
        GameObject obj = new GameObject("Texto", typeof(RectTransform));
        obj.transform.SetParent(pai, false);

        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(900f, 150f);
        rt.anchoredPosition = pos;

        TextMeshProUGUI t = obj.AddComponent<TextMeshProUGUI>();
        t.text = conteudo;
        t.fontSize = tamanho;
        t.color = Color.white;
        t.fontStyle = FontStyles.Bold;
        t.alignment = TextAlignmentOptions.Center;
        t.raycastTarget = false;
    }

    void CriarBotao(Transform pai, string conteudo, Vector2 pos, UnityEngine.Events.UnityAction acao)
    {
        GameObject obj = new GameObject("Botao_" + conteudo, typeof(RectTransform));
        obj.transform.SetParent(pai, false);

        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(500f, 120f);
        rt.anchoredPosition = pos;

        Image img = obj.AddComponent<Image>();
        img.color = new Color(0.45f, 0.25f, 0.1f, 1f); // marrom madeira

        Button botao = obj.AddComponent<Button>();
        botao.targetGraphic = img;
        botao.onClick.AddListener(acao);

        CriarTexto(obj.transform, conteudo, 55f, Vector2.zero);
    }
}