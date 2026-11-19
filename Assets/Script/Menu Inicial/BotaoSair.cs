
using UnityEngine;

public class BotaoSair : MonoBehaviour
{
    // Fecha o jogo
    public void SairDoJogo()
    {
        Application.Quit();

        // Permite testar o botão dentro da Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
