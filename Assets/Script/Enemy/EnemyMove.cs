using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private float speed = 10f;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f); //Destroy the enemy after 5 seconds
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        speed = GameManager.Instance.speed;
    }

    void UnfreezeY()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Projectile"))
        {
            UnfreezeY();
        }
    }
}
