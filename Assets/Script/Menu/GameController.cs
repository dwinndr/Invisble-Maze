using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public string[] gameScenes;  // Array untuk menyimpan nama-nama scene yang akan dipilih secara acak
    public GameObject point3;
    public GameObject point5;
    public GameObject point7;
    public GameObject playButton;
    public GameObject exitButton;
    public GameObject selectScoreHolder;
    public GameObject htpButton;
    public GameObject htpPanel;
    public GameObject title;
    private AudioSource sfxClick;

    private void Start()
    {
        sfxClick = GetComponent<AudioSource>();

        if (selectScoreHolder)
        {
            selectScoreHolder.SetActive(false);
        }
    }
    
    private void SfxClick()
    {
        sfxClick.Play();
    }
    public void ThreePoint()
    {
        SfxClick();
        ScoreManager.instance.totalScore = 3;
    }

    public void FivePoint()
    {
        SfxClick();
        ScoreManager.instance.totalScore = 5;
    }

    public void SevenPoint()
    {
        SfxClick();
        ScoreManager.instance.totalScore = 7;
    }

    public void PlayGame()
    {
        if (gameScenes.Length == 0)
        {
            Debug.LogError("Tidak ada scene yang ditentukan dalam gameScenes.");
            return;
        }

        // Pilih scene secara acak
        int randomIndex = Random.Range(0, gameScenes.Length);
        string selectedScene = gameScenes[randomIndex];

        // Pindah ke scene yang dipilih
        SceneManager.LoadScene(selectedScene);
        
        SfxClick();
    }

    public void SelectScore()
    {
        playButton.SetActive(false);
        exitButton.SetActive(false);
        selectScoreHolder.SetActive(true);
        title.SetActive(false);

        SfxClick();
    }

    public void Back()
    {
        playButton.SetActive(true);
        exitButton.SetActive(true);
        selectScoreHolder.SetActive(false);
        title.SetActive(true);
        
        SfxClick();
    }

    public void Exit()
    {
        Application.Quit();

        SfxClick();
    }

    public void HTP()
    {
        htpButton.SetActive(false);
        htpPanel.SetActive(true);

        SfxClick();
    }

    public void BackHTP()
    {
        htpButton.SetActive(true);
        htpPanel.SetActive(false);

        SfxClick();
    }
}
