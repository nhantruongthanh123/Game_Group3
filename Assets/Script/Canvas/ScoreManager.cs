using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI scoreText; // Tham chiếu đến Text UI
    private int score = 0; // Biến lưu điểm
    void Start()
    {
        UpdateScoreText();
        InvokeRepeating("IncreaseScore", 0.1f, 0.1f); // Tăng điểm mỗi giây
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) // Nếu game over
        {
            CancelInvoke("IncreaseScore"); // Hủy việc tăng điểm
        }
    }


    void IncreaseScore()
    {
        score++;
        UpdateScoreText(); 
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
