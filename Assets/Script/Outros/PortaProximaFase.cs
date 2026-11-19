
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PortaProximaFase : MonoBehaviour
{
    [Header("Bloqueio da porta")]
    public TilemapCollider2D bloqueioPorta;

    [Header("Coletáveis")]
    public int itensNecessarios = 10;

    private bool faseCarregando = false;

    void Update()
    {
        // Libera a porta após 10 coletas.
        if (MovimentoPersonagem.quantidadeColetada >= itensNecessarios)
        {
            if (bloqueioPorta != null)
                bloqueioPorta.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        VerificarPorta(outro);
    }

    private void OnTriggerStay2D(Collider2D outro)
    {
        VerificarPorta(outro);
    }

    private void VerificarPorta(Collider2D outro)
    {
        if (!outro.CompareTag("Player") || faseCarregando)
            return;

        if (MovimentoPersonagem.quantidadeColetada >= itensNecessarios)
        {
            faseCarregando = true;
            SceneManager.LoadScene("Fase2");
        }
        else
        {
            Debug.Log("Colete pelo menos 10 cenouras!");
        }
    }
}
