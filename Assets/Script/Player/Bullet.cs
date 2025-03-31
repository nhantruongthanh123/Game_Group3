using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bullet : MonoBehaviour
{
    private float speed = 20f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject hit_effect;
    [SerializeField] private GameObject explosion_effect;
    private bool isHit = true;
    private float maxHitTime = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D col) {

		if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Asteroid")) {
			
            if(col.gameObject.CompareTag("Enemy")) {
                if(isHit) {
                    ScoreManager.Instance.score += 20;
                }
                StartCoroutine(HitTimerCount());
                Instantiate(hit_effect, transform.position, Quaternion.identity);
            }
            else if(col.gameObject.CompareTag("Asteroid")) {
                if(isHit) {
                    ScoreManager.Instance.score += 10;
                }
                StartCoroutine(HitTimerCount());
                Instantiate(explosion_effect, transform.position, Quaternion.identity);
            }
			Destroy(gameObject);
		}


	}

    System.Collections.IEnumerator HitTimerCount() {
        isHit = false;
        yield return new WaitForSeconds(maxHitTime);
        isHit = true;
    }
}
