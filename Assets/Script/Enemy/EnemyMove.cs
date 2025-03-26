using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
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
