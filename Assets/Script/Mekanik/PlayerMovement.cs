using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    public HealthScoring healthLah;
    public GameObject playerObject;
    public GameObject finishObject;
    public List<GameObject> tileDots;
    public float moveDistance = 1.0f;
    public float movementSpeed = 5.0f;

    private Vector3 initialPlayerPosition;
    private Vector3 savedPlayerPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isResetting = false;
    public int currentPlayer; // Indikator pemain yang sedang mengontrol
    private Container containerScript; // Referensi ke script Container
    private CurrentPlayer playerNow;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        playerNow = FindObjectOfType<CurrentPlayer>();
        currentPlayer = playerNow.currentPlayer;
        containerScript = FindObjectOfType<Container>(); // Cari script Container di scene

        if (playerObject == null || finishObject == null || tileDots == null || tileDots.Count < 2)
        {
            Debug.LogError("PlayerObject, FinishObject, atau tileDots belum di-set dengan benar.");
            return;
        }

        PlacePrefabsRandomly();
        SavePlayerPosition();
    }

    private void SoundClick()
    {
        audioSource.Play();
        Debug.Log("play sfx");
    }

    void PlacePrefabsRandomly()
    {
        if (tileDots.Count < 2)
        {
            Debug.LogError("Tidak cukup tile dots untuk menempatkan player dan finish.");
            return;
        }

        List<int> randomIndices = new List<int>();
        while (randomIndices.Count < 2)
        {
            int randomIndex = Random.Range(0, tileDots.Count);
            if (!randomIndices.Contains(randomIndex))
            {
                randomIndices.Add(randomIndex);
            }
        }

        int playerIndex = randomIndices[0];
        playerObject.transform.position = tileDots[playerIndex].transform.position;

        initialPlayerPosition = playerObject.transform.position;

        int finishIndex = randomIndices[1];
        finishObject.transform.position = tileDots[finishIndex].transform.position;
    }

    public void MoveUp()
    {
        if (currentPlayer == 1)
            Move(Vector3.up);
            SoundClick();
    }

    public void MoveDown()
    {
        if (currentPlayer == 1)
            Move(Vector3.down);
            SoundClick();
    }

    public void MoveLeft()
    {
        if (currentPlayer == 1)
            Move(Vector3.left);
            SoundClick();
    }

    public void MoveRight()
    {
        if (currentPlayer == 1)
            Move(Vector3.right);
            SoundClick();
    }

    public void Player2MoveUp()
    {
        if (currentPlayer == 2)
            Move(Vector3.up);
            SoundClick();
    }

    public void Player2MoveDown()
    {
        if (currentPlayer == 2)
            Move(Vector3.down);
            SoundClick();
    }

    public void Player2MoveLeft()
    {
        if (currentPlayer == 2)
            Move(Vector3.left);
            SoundClick();
    }

    public void Player2MoveRight()
    {
        if (currentPlayer == 2)
            Move(Vector3.right);
            SoundClick();
    }

    private void Move(Vector3 direction)
    {
        if (playerObject == null)
        {
            Debug.LogError("Player Object belum di-set.");
            return;
        }

        GameObject closestTile = null;
        float shortestDistance = float.MaxValue;

        foreach (GameObject tile in tileDots)
        {
            Vector3 tilePosition = tile.transform.position;
            Vector3 projectedPosition = playerObject.transform.position + direction * moveDistance;

            float distance = Vector3.Distance(tilePosition, projectedPosition);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestTile = tile;
            }
        }

        if (closestTile != null)
        {
            targetPosition = closestTile.transform.position;
            isMoving = true;
            isResetting = false; // Reset flag saat mulai bergerak
        }
        else
        {
            Debug.LogWarning("Tidak ada tile dot terdekat yang ditemukan.");
        }
    }

    private void Update()
    {
        if (isMoving && !isResetting)
        {
            playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, targetPosition, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(playerObject.transform.position, targetPosition) < 0.001f)
            {
                isMoving = false;
            }
        }
    }

    public void SavePlayerPosition()
    {
        savedPlayerPosition = playerObject.transform.position;
    }

    public void ResetToSavedPosition()
    {
        playerObject.transform.position = savedPlayerPosition;
        isMoving = false;
        isResetting = true;
        SwitchPlayer(); // Panggil fungsi untuk berganti pemain
    }

    public void ResetPlayerPosition()
    {
        playerObject.transform.position = initialPlayerPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            if (currentPlayer == 1)
            {
                Debug.Log("Player 1 menabrak Wall");
                healthLah.TakeDamage1(1);
            }
            else
            {
                Debug.Log("Player 2 menabrak Wall");
                healthLah.TakeDamage2(1);
            }
            containerScript.CheckWallHit(other.gameObject); // Panggil fungsi untuk cek wall mana yang ditabrak
            ResetToSavedPosition();
        }

        if (other.CompareTag("Finish"))
        {
            if (currentPlayer == 1)
            {
                containerScript.CheckFinishHit1();
                playerNow.currentPlayer = 1;
            }
            else
            {
                containerScript.CheckFinishHit2();
                playerNow.currentPlayer = 2;
            }
        }
    }

    private void SwitchPlayer()
    {
        currentPlayer = currentPlayer == 1 ? 2 : 1;
    }
}
