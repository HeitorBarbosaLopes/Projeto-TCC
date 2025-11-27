using UnityEngine;

public class ExitBtt : MonoBehaviour
{
    // Esta função será chamada pelo botão
    public void FecharJogo()
    {
        // Exibe uma mensagem no console para confirmar que funcionou
        Debug.Log("O jogo está fechando...");

        // Lógica condicional:
#if UNITY_EDITOR
        // Se estiver rodando dentro do editor da Unity, para o "Play Mode"
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Se for o jogo compilado (PC, Celular, etc), fecha o aplicativo
            Application.Quit();
#endif
    }
}