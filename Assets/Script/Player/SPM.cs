using UnityEngine;

public class SPM : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject hit_effect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f); 
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D col) {

		if (!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("Projectile") && !col.gameObject.CompareTag("Barrier")) {
			Instantiate(hit_effect, transform.position, Quaternion.identity);
            Destroy(col.gameObject); // Destroy the enemy
		}
	}
}
