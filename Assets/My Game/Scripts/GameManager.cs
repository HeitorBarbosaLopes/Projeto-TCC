using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Progresso")]
    public int currentDay = 1;
    public int totalScore = 0;
    public int dailyScore = 0;
    private const int MaxDays = 3;

    [Header("UI & Visual")]
    public GameObject nightOverlay;
    public GameObject resultScreen;
    public TextMeshProUGUI scoreText;
    public GameObject nextDayButton; // Referência ao botão "Continuar"

    [Header("Estado")]
    public bool isNight = false;
    public bool workDone = false;

    void Awake()
    {
        if (instance == null) instance = this;
        StartDay();
    }

    public void StartDay()
    {
        isNight = false;
        workDone = false;
        dailyScore = 0;

        nightOverlay.SetActive(false);
        resultScreen.SetActive(false);

        Debug.Log("--- INÍCIO DO DIA " + currentDay + " ---");
    }

    public void FinishWork()
    {
        workDone = true;
        isNight = true;
        nightOverlay.SetActive(true);
        Debug.Log("Trabalho encerrado. Noite chegou.");
    }

    public void GoToSleep()
    {
        if (!isNight) return;
        if (resultScreen.activeSelf) return;

        totalScore += dailyScore;
        ShowDailyResult();
    }

    void ShowDailyResult()
    {
        resultScreen.SetActive(true);

        if (currentDay >= MaxDays)
        {
            // É o fim do jogo (Dia 3 completado)
            scoreText.text = "FIM DE JOGO!\n\nPONTUACAO TOTAL: " + totalScore + " / 375" + "\nOBRIGADO POR JOGAR!";

            // Desativa o botão de "Próximo Dia" pois não há mais dias
            if (nextDayButton != null) nextDayButton.SetActive(false);
        }
        else
        {
            // É apenas o fim de um dia comum
            scoreText.text = "DIA " + currentDay + " COMPLETO.\n\nPONTOS HOJE: " + dailyScore + "\nTOTAL ACUMULADO: " + totalScore;

            // Garante que o botão esteja visível
            if (nextDayButton != null) nextDayButton.SetActive(true);
        }
    }

    public void NextDayButton()
    {
        if (currentDay < MaxDays)
        {
            currentDay++;
            StartDay();
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void AddScore(int points)
    {
        dailyScore += points;
    }
}