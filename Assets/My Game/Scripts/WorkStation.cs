using UnityEngine;
using TMPro;
using System.Collections.Generic; // Necessário para usar Listas

public class WorkStation : MonoBehaviour
{
    [System.Serializable]
    public struct WorkTask
    {
        public string senderName;
        [TextArea] public string message;
        public string optionBad;
        public string optionMed;
        public string optionGood;
    }

    [Header("Configuração das Tarefas")]
    public WorkTask[] day1Tasks;
    public WorkTask[] day2Tasks;
    public WorkTask[] day3Tasks;

    private WorkTask[] currentDayTasks;

    [Header("Interface")]
    public GameObject computerScreenUI;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI senderText;

    // Textos dos botões
    public TextMeshProUGUI btnBadText, btnMedText, btnGoodText;

    [Header("Posicionamento dos Botões")]
    // ARRASTE OS BOTÕES INTEIROS (o objeto que tem o componente Button) PARA CÁ
    public RectTransform btnBadRect;
    public RectTransform btnMedRect;
    public RectTransform btnGoodRect;

    // Lista para guardar as coordenadas originais
    private List<Vector2> possiblePositions = new List<Vector2>();

    private int currentTaskIndex = 0;
    private bool positionsSaved = false;

    void Start()
    {
        // Salva as posições iniciais dos botões apenas uma vez
        if (!positionsSaved)
        {
            possiblePositions.Add(btnBadRect.anchoredPosition);
            possiblePositions.Add(btnMedRect.anchoredPosition);
            possiblePositions.Add(btnGoodRect.anchoredPosition);
            positionsSaved = true;
        }
    }

    public void OpenComputer()
    {
        if (GameManager.instance.workDone) return;

        int day = GameManager.instance.currentDay;

        if (day == 1) currentDayTasks = day1Tasks;
        else if (day == 2) currentDayTasks = day2Tasks;
        else if (day == 3) currentDayTasks = day3Tasks;
        else currentDayTasks = day3Tasks;

        if (currentDayTasks.Length == 0)
        {
            CloseComputer();
            return;
        }

        computerScreenUI.SetActive(true);
        currentTaskIndex = 0;
        LoadTask();

        Time.timeScale = 0f;
    }

    void LoadTask()
    {
        if (currentTaskIndex < currentDayTasks.Length)
        {
            WorkTask t = currentDayTasks[currentTaskIndex];

            senderText.text = "De: " + t.senderName;
            messageText.text = t.message;

            btnBadText.text = t.optionBad;
            btnMedText.text = t.optionMed;
            btnGoodText.text = t.optionGood;

            // --- AQUI A MÁGICA ACONTECE: MISTURAR OS BOTÕES ---
            ShuffleButtonPositions();
        }
        else
        {
            CloseComputer();
        }
    }

    void ShuffleButtonPositions()
    {
        // 1. Cria uma cópia da lista de posições para podermos bagunçar
        List<Vector2> tempPositions = new List<Vector2>(possiblePositions);

        // 2. Função auxiliar para pegar uma posição aleatória e remover da lista
        Vector2 GetRandomPos()
        {
            int randomIndex = Random.Range(0, tempPositions.Count);
            Vector2 pos = tempPositions[randomIndex];
            tempPositions.RemoveAt(randomIndex);
            return pos;
        }

        // 3. Atribui uma posição aleatória para cada botão
        btnBadRect.anchoredPosition = GetRandomPos();
        btnMedRect.anchoredPosition = GetRandomPos();
        btnGoodRect.anchoredPosition = GetRandomPos();
    }

    public void ChooseOption(int quality)
    {
        int points = 0;
        switch (quality)
        {
            case 0: points = 5; break;
            case 1: points = 10; break;
            case 2: points = 25; break;
        }

        GameManager.instance.AddScore(points);

        currentTaskIndex++;
        LoadTask();
    }

    void CloseComputer()
    {
        computerScreenUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.FinishWork();
    }
}