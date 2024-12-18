using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI score1Text;
    public TextMeshProUGUI score2Text;
    public static ScoreManager instance { get; private set; }
    private int score1 = 0;
    private int score2 = 0;
    public int totalScore = 10;

    private PauseMenu pauseMenuScript;
    private Container containerScript;

    private void Awake()
    {
        // Ensure there's only one instance of ScoreManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        // Check if the current scene is the Main Menu
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            // Deactivate TextMeshProUGUI components in Main Menu
            score1Text.gameObject.SetActive(false);
            score2Text.gameObject.SetActive(false);
        }
        else
        {
            // Activate TextMeshProUGUI components in other scenes
            score1Text.gameObject.SetActive(true);
            score2Text.gameObject.SetActive(true);
            UpdateScoreText();
        }

        // Subscribe to scene loading event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from scene loading event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void AddScore1()
    {
        Debug.Log("Spasiiii");
        score1++;
        UpdateScoreText();
    }

    public void AddScore2()
    {
        Debug.Log("Spasiiii");
        score2++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        score1Text.text = "" + score1;
        score2Text.text = "" + score2;
    }

    public void SetScore(int newScore1, int newScore2)
    {
        score1 = newScore1;
        score2 = newScore2;
        UpdateScoreText();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find TextMeshProUGUI components in the new scene and set their scores
        GameObject score1Object = GameObject.Find("Score1");
        GameObject score2Object = GameObject.Find("Score2");

        if (score1Object != null)
        {
            TextMeshProUGUI newScore1Text = score1Object.GetComponent<TextMeshProUGUI>();
            if (newScore1Text != null)
            {
                newScore1Text.text = "" + score1;
            }
        }

        if (score2Object != null)
        {
            TextMeshProUGUI newScore2Text = score2Object.GetComponent<TextMeshProUGUI>();
            if (newScore2Text != null)
            {
                newScore2Text.text = "" + score2;
            }
        }

        // Ensure correct visibility of TextMeshProUGUI components based on scene
        if (scene.name == "Main Menu")
        {
            if (score1Object != null) score1Object.SetActive(false);
            if (score2Object != null) score2Object.SetActive(false);
            score1 = 0;
            score2 = 0;
        }
        else
        {
            AssignScoreText();
            if (score1Object != null) score1Object.SetActive(true);
            if (score2Object != null) score2Object.SetActive(true);
        }
    }

    private void AssignScoreText()
    {
        if (score1Text == null || score2Text == null)
        {
            // Cari dan assign TextMeshProUGUI berdasarkan nama GameObject atau tag
            GameObject tempScore1 = GameObject.Find("Score1");
            GameObject tempScore2 = GameObject.Find("Score2");

            if (tempScore1 != null)
            {
                score1Text = tempScore1.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("GameObject dengan nama 'Score1' tidak ditemukan.");
            }

            if (tempScore2 != null)
            {
                score2Text = tempScore2.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("GameObject dengan nama 'Score2' tidak ditemukan.");
            }
        }
    }

    public void Update()
    {
        if (score1 == totalScore)
        {
            pauseMenuScript = FindObjectOfType<PauseMenu>();
            pauseMenuScript.textWin.gameObject.SetActive(true);
            pauseMenuScript.textWin.text = "Player 1 Win";
            pauseMenuScript.resumeButton.SetActive(false);
            pauseMenuScript.pauseMenu.SetActive(true);
            pauseMenuScript.pauseButton.SetActive(false);
            Debug.Log("PAUSE MENU AKTIF");
            containerScript = FindObjectOfType<Container>();
            containerScript.ContainerbuttonP1.SetActive(false);
            containerScript.ContainerbuttonP2.SetActive(false);
            containerScript.readyHolder.SetActive(false);
        }
        else if (score2 == totalScore)
        {
            pauseMenuScript = FindObjectOfType<PauseMenu>();
            pauseMenuScript.textWin.gameObject.SetActive(true);
            pauseMenuScript.textWin.text = "Player 2 Win";
            pauseMenuScript.resumeButton.SetActive(false);
            pauseMenuScript.pauseMenu.SetActive(true);
            pauseMenuScript.pauseButton.SetActive(false);
            Debug.Log("PAUSE MENU AKTIF");
            containerScript = FindObjectOfType<Container>();
            containerScript.ContainerbuttonP1.SetActive(false);
            containerScript.ContainerbuttonP2.SetActive(false);
            containerScript.readyHolder.SetActive(false);
        }
    }

    public int GetScore1()
    {
        return score1;
    }

    public int GetScore2()
    {
        return score2;
    }
}
