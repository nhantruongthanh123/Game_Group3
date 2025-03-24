using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 2f; 

    void Start()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x <= -13)
        {
            Destroy(gameObject);
        }
    }
}
