using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Container : MonoBehaviour
{
    public static Container instance;
    public GameObject containerObject;  // GameObject container yang akan dirotasi
    public GameObject[] walls;          // Array yang berisi referensi ke Wall
    public TextMeshProUGUI countdownText1; // Referensi ke TextMeshPro pertama untuk menampilkan hitungan mundur
    public TextMeshProUGUI countdownText2; // Referensi ke TextMeshPro kedua untuk menampilkan hitungan mundur
    public GameObject ContainerbuttonP1;
    public GameObject ContainerbuttonP2;
    public GameObject Player;
    public GameObject Finish;
    public TextMeshProUGUI playerWinText1;
    public TextMeshProUGUI playerWinText2;
    public GameObject readyHolder;
    public GameObject ceklis1;
    public GameObject ceklis2;
    private bool ready1 = false;
    private bool ready2 = false;

    public float countdownTime;   // Waktu mundur sebelum Wall menghilang
    public float fadeDuration = 2f;     // Durasi waktu untuk Wall memudar
    public string[] nextSceneNames;

    private void Awake()
    {
        // Rotasi acak pada container
        RotateContainer();

        // Mulai menghitung mundur dan memudarkan Wall
        StartCoroutine(StartCountdownAndFadeWalls());
    }

    void Start()
    {
        if (readyHolder)
        {
            readyHolder.SetActive(false);
        }
        ready1 = false;
        ready2 = false;
        if (ContainerbuttonP1 && ContainerbuttonP2 && Player && Finish)
        {
            ContainerbuttonP1.SetActive(false);
            ContainerbuttonP2.SetActive(false);
            Player.SetActive(false);
            Finish.SetActive(false);
        }
    }

    void RotateContainer()
    {
        if (containerObject == null)
        {
            Debug.LogError("Container Object belum di-set.");
            return;
        }

        // Pilihan rotasi pada sumbu Z
        float[] possibleRotations = { 0f, 90f, 180f, 360f };

        // Pilih rotasi acak
        float randomRotationZ = possibleRotations[Random.Range(0, possibleRotations.Length)];

        // Terapkan rotasi pada sumbu Z
        containerObject.transform.rotation = Quaternion.Euler(0f, 0f, randomRotationZ);
    }

    IEnumerator StartCountdownAndFadeWalls()
    {
        float timeRemaining = countdownTime;

        while (timeRemaining > 0)
        {
            // Update kedua UI TextMeshPro dengan waktu yang tersisa
            if (countdownText1 != null)
            {
                countdownText1.text = "" + Mathf.CeilToInt(timeRemaining).ToString();
            }
            if (countdownText2 != null)
            {
                countdownText2.text = "" + Mathf.CeilToInt(timeRemaining).ToString();
            }

            // Tunggu selama satu detik
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }

        // Setelah hitungan mundur selesai, destroy text
        Destroy(countdownText1);
        Destroy(countdownText2);
        ContainerbuttonP1.SetActive(true);
        ContainerbuttonP2.SetActive(true);
        Player.SetActive(true);
        Finish.SetActive(true);

        // Mulai memudarkan tiap Wall
        foreach (GameObject wall in walls)
        {
            StartCoroutine(FadeOutWall(wall));
        }
    }

    IEnumerator FadeOutWall(GameObject wall)
    {
        if (wall == null)
        {
            yield break;
        }

        SpriteRenderer wallRenderer = wall.GetComponent<SpriteRenderer>();
        if (wallRenderer == null)
        {
            Debug.LogError("Wall tidak memiliki komponen SpriteRenderer.");
            yield break;
        }

        Color originalColor = wallRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeDuration);
            wallRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Pastikan alpha benar-benar 0 setelah fade selesai
        wallRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    public void CheckWallHit(GameObject hitWall)
    {
        SpriteRenderer wallRenderer = hitWall.GetComponent<SpriteRenderer>();
        if (wallRenderer != null)
        {
            // Set alpha wall menjadi 255 (opacity penuh)
            Color newColor = wallRenderer.color;
            newColor.a = 1f; // 255 dalam skala 0-1 untuk Unity
            wallRenderer.color = newColor;
        }
        else
        {
            Debug.LogError("Wall yang ditabrak tidak memiliki komponen SpriteRenderer.");
        }
    }

    public void CheckFinishHit1()
    {
        playerWinText1.gameObject.SetActive(true);
        playerWinText2.gameObject.SetActive(true);
        playerWinText1.text = "Player 1 Win";
        playerWinText2.text = "Player 1 Win";
        ScoreManager.instance.AddScore1();
        readyHolder.SetActive(true);
    }

    public void CheckFinishHit2()
    {
        playerWinText1.gameObject.SetActive(true);
        playerWinText2.gameObject.SetActive(true);
        playerWinText1.text = "Player 2 Win";
        playerWinText2.text = "Player 2 Win";
        ScoreManager.instance.AddScore2();
        readyHolder.SetActive(true);
    }

    public void ReadyButton1()
    {
        if (!ready1)
        {
            ready1 = true;
            ceklis1.SetActive(true);
        }
    }

    public void ReadyButton2()
    {
        if (!ready2)
        {
            ready2 = true;
            ceklis2.SetActive(true);
        }
    }

    private void Update()
    {
        if (ready1 && ready2)
        {
            LoadRandomScene();
        }
    }

    private void LoadRandomScene()
    {
        if (nextSceneNames.Length == 0)
        {
            Debug.LogError("Daftar nextSceneNames kosong.");
            return;
        }

        string currentSceneName = SceneManager.GetActiveScene().name;

        // Memilih scene acak yang berbeda dari scene saat ini
        string randomSceneName;
        do
        {
            randomSceneName = nextSceneNames[Random.Range(0, nextSceneNames.Length)];
        } while (randomSceneName == currentSceneName);

        SceneManager.LoadScene(randomSceneName);
    }
}
