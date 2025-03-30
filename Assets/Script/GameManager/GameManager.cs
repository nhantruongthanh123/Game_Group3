using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance;
    public bool isGameOver = false;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button HomeButton;
    [SerializeField] private Button backToMenuButton; 
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    public float speed = 8f; 
    private float timeElapsed = 0;
    void Start()
    {
        PauseButton.onClick.AddListener(PauseGame);
        ResumeButton.onClick.AddListener(ResumeGame);
        HomeButton.onClick.AddListener(BackToMenu);
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

        if (speed < 30f)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= 5.0f) {
                speed += 1;
                timeElapsed = 0;
            }
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

    void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true); 

    }
    void ResumeGame()
    {
        Time.timeScale = 1; 
        pausePanel.SetActive(false);
    }
}
