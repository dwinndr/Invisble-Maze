using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthScoring : MonoBehaviour
{
    public static HealthScoring instance;
    private ScoreManager scoreManager;
    private PauseMenu pauseMenuScriptt;
    [SerializeField] private float startingHealth;
    public float currentHealth1 { get; private set; }
    public float currentHealth2 { get; private set; }
    public PlayerMovement currentPlayer;
    public TextMeshProUGUI text1Win;
    public TextMeshProUGUI text2Win;

    void Awake()
    {
        currentHealth1 = startingHealth;
        currentHealth2 = startingHealth;
    }

    public void TakeDamage1(float _damage)
    {
        currentHealth1 = Mathf.Clamp(currentHealth1 - _damage, 0, startingHealth);

        if (currentHealth1 > 0)
        {
            // Player 1 masih hidup
            Debug.Log("Player 1 took damage, remaining health: " + currentHealth1);
        }
        else
        {
            // Player 1 kalah
            if (currentPlayer.currentPlayer == 1 && currentHealth1 == 0 && currentHealth2 == 0)
            {
                HandlePlayerDefeat();
            }
            else if (currentPlayer.currentPlayer == 2 && currentHealth1 == 0 && currentHealth2 == 0)
            {
                HandlePlayerDefeat();
            }
        }
    }

    public void TakeDamage2(float _damage)
    {
        currentHealth2 = Mathf.Clamp(currentHealth2 - _damage, 0, startingHealth);

        if (currentHealth2 > 0)
        {
            // Player 2 masih hidup
            Debug.Log("Player 2 took damage, remaining health: " + currentHealth2);
        }
        else
        {
            // Player 1 kalah
            if (currentPlayer.currentPlayer == 1 && currentHealth1 == 0 && currentHealth2 == 0)
            {
                HandlePlayerDefeat();
            }
            else if (currentPlayer.currentPlayer == 2 && currentHealth1 == 0 && currentHealth2 == 0)
            {
                HandlePlayerDefeat();
            }
        }
    }

    private void HandlePlayerDefeat()
    {
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }
        // Mengecek skor untuk menentukan pemenang
        if (scoreManager != null)
        {
            if (scoreManager.GetScore1() > scoreManager.GetScore2())
            {
                Debug.Log("Player 1 Win");
                pauseMenuScriptt = FindObjectOfType<PauseMenu>();
                pauseMenuScriptt.textWin.gameObject.SetActive(true);
                pauseMenuScriptt.pauseMenu.SetActive(true);
                pauseMenuScriptt.resumeButton.SetActive(false);
                pauseMenuScriptt.pauseButton.SetActive(false);
                pauseMenuScriptt.textWin.text = "Player 1 Win";
            }
            else if (scoreManager.GetScore2() > scoreManager.GetScore1())
            {
                Debug.Log("Player 2 Win");
                pauseMenuScriptt = FindObjectOfType<PauseMenu>();
                pauseMenuScriptt.textWin.gameObject.SetActive(true);
                pauseMenuScriptt.pauseMenu.SetActive(true);
                pauseMenuScriptt.resumeButton.SetActive(false);
                pauseMenuScriptt.pauseButton.SetActive(false);
                pauseMenuScriptt.textWin.text = "Player 2 Win";
            }
            else
            {
                // Jika skor imbang, mungkin Anda ingin menampilkan pesan imbang atau melakukan sesuatu yang lain
                Debug.Log("It's a tie!");
                pauseMenuScriptt = FindObjectOfType<PauseMenu>();
                pauseMenuScriptt.textWin.gameObject.SetActive(true);
                pauseMenuScriptt.pauseMenu.SetActive(true);
                pauseMenuScriptt.resumeButton.SetActive(false);
                pauseMenuScriptt.pauseButton.SetActive(false);
                pauseMenuScriptt.textWin.text = "It's a tie!";
            }
        }

        // Mengaktifkan Pause Menu (jika ada)
        // PauseMenu pauseMenuScript = FindObjectOfType<PauseMenu>();
        // if (pauseMenuScript != null)
        // {
        // pauseMenuScriptt.pauseMenu.SetActive(true);
        // pauseMenuScriptt.resumeButton.SetActive(false);
        // }
        // else
        // {
        //     Debug.LogError("PauseMenu script not found in the scene!");
        // }
    }
}
