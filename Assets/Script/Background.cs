using UnityEngine;
using System.Collections.Generic;

public class Background : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Transform> backgrounds; 
    public float speed = 2f; // Tốc độ di chuyển
    private float width; // Chiều rộng của mỗi phần nền
    void Start()
    {
        width = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) // Nếu game over
        {
            return;
        }

        speed = GameManager.Instance.speed; 

        foreach (var bg in backgrounds)
        {
            bg.position += Vector3.left * speed * Time.deltaTime;
        }

        if (backgrounds[0].position.x <= -width * 3 / 2)
        {
            Transform firstBg = backgrounds[0];
            firstBg.position = new Vector3(backgrounds[backgrounds.Count - 1].position.x + width, firstBg.position.y, firstBg.position.z);

            backgrounds.RemoveAt(0);
            backgrounds.Add(firstBg);
        }
    }
}
