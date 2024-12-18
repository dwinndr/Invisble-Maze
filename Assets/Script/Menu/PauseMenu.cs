using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject[] buttonHolder;
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject resumeButton;
    public TextMeshProUGUI textWin;
    private Container containerScript;

    // Start is called before the first frame update
    void Start()
    {
        containerScript = FindObjectOfType<Container>();
        if (pauseMenu)
        {
            pauseMenu.SetActive(false);
        }
        if (pauseButton)
        {
            pauseButton.SetActive(false);
            StartCoroutine(ActivatePauseButtonAfterDelay(containerScript.countdownTime)); // Memulai coroutine untuk mengaktifkan pauseButton setelah 10 detik
        }
        if (textWin)
        {
            textWin.gameObject.SetActive(false);
        }
    }

    IEnumerator ActivatePauseButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Menunggu selama 'delay' detik
        if (pauseButton)
        {
            pauseButton.SetActive(true); // Mengaktifkan pauseButton setelah 10 detik
        }
    }

    public void Menu()
    {
        pauseMenu.SetActive(true);
        textWin.gameObject.SetActive(false);
        pauseButton.SetActive(false);
        foreach (GameObject button in buttonHolder)
        {
            if (button)
            {
                button.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        foreach (GameObject button in buttonHolder)
        {
            if (button!)
            {
                button.SetActive(true);
            }
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }


}
