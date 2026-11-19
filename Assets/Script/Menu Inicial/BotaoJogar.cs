using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotaoJogar : MonoBehaviour
{
    [Header("Configurações da Cena")]
    [Tooltip("Nome exato da cena que será carregada")]
    [SerializeField] private string nomeDaFase = "Fase1";

    private Button botao;

    private void Awake()
    {
        // Pega o componente Button que está neste mesmo objeto
        botao = GetComponent<Button>();

        // Conecta o evento de clique via código
        if (botao != null)
        {
            botao.onClick.AddListener(CarregarFase);
        }
        else
        {
            Debug.LogError("Nenhum componente Button encontrado neste objeto!");
        }
    }

    public void CarregarFase()
    {
        SceneManager.LoadScene(nomeDaFase);
    }

    private void OnDestroy()
    {
        // Boa prática: limpa o listener quando o objeto é destruído
        if (botao != null)
        {
            botao.onClick.RemoveListener(CarregarFase);
        }
    }
}