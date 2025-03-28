using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance;
    public bool isGameOver = false;
    public Button restartButton; 
    public Button backToMenuButton; 
    public GameObject gameOverPanel;
    public float speed = 8f; 
    private float timeElapsed = 0;
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        backToMenuButton.onClick.AddListener(BackToMenu);
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
        }

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 5.0f) {
            speed += 1;
            timeElapsed = 0;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void RestartGame()
    {
        isGameOver = false; // Reset the game over state
        gameOverPanel.SetActive(false); // Hide the game over panel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
        restartButton.onClick.AddListener(RestartGame);
        backToMenuButton.onClick.AddListener(BackToMenu);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene"); // Load the main menu scene
        isGameOver = false; // Reset the game over state
        gameOverPanel.SetActive(false); // Hide the game over panel
        restartButton.onClick.AddListener(RestartGame);
        backToMenuButton.onClick.AddListener(BackToMenu);
    }
}
