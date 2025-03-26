using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject hit_effect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f); //Bullet will despawn after 5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D col) {

		//Don't want to collide with the ship that's shooting this thing, nor another projectile.
		if (!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("Projectile")) {
			Instantiate(hit_effect, transform.position, Quaternion.identity);
            
			Destroy(gameObject);
		}
	}
}
