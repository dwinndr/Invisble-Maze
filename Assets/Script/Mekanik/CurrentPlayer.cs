using UnityEngine.SceneManagement;
using UnityEngine;

public class CurrentPlayer : MonoBehaviour
{
    public static CurrentPlayer instance;
    public int currentPlayer = 1;

    void Start()
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

    void Update()
    {
        CheckIfMainMenu();
    }

    void CheckIfMainMenu()
    {
        // Check if the current scene is the Main Menu
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            currentPlayer = 1;
        }
    }
}
