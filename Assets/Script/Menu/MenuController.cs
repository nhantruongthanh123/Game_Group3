using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button startButton;
    public Button howToPlayButton;
    public GameObject howToPlayText1;
    public GameObject howToPlayText2;
    public Button exitButton;
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        howToPlayButton.onClick.AddListener(ToggleHowToPlay);
        exitButton.onClick.AddListener(QuitGame);
        howToPlayText1.SetActive(false);
        howToPlayText2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("WuannScene"); 
    }
    public void ToggleHowToPlay()
    {
        bool isActive = !howToPlayText1.activeSelf;
        howToPlayText1.SetActive(isActive);
        howToPlayText2.SetActive(isActive);
    }
    public void QuitGame()
    {
        Debug.Log("Quit game..."); // Khi bấm nút exit sẽ hiện dòng này trên console
        Application.Quit();
    }
}
