using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PLayer : MonoBehaviour
{
    [SerializeField] private GameObject myBody;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform pos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject timerText; // Reference to the UI Text
    [SerializeField] private GameObject SPM_missile;
    [SerializeField] private GameObject hit_effect;
    private float shieldDuration = 5f;
    private bool isShieldActive = false;
    private float shieldTimer = 0f;
    private float maxShieldTime = 10f;
    private float maxMissileScale = 25f;
    private float SPM_timer = 0f;
    private float maxSPM_time = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = myBody.GetComponent<Rigidbody2D>();
        pos = myBody.GetComponent<Transform>();
        shield.SetActive(false);
        StartCoroutine(ShieldTimerCount());
        StartCoroutine(SPMTimerCount());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        Shield();
        SPM();
    }

    public void Move() 
    {
        float horizontalMovement = 0f;
        if(Input.GetKey(KeyCode.A)) 
        {
            //rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            //pos.position += moveSpeed * Time.deltaTime * Vector3.left;
            horizontalMovement = -moveSpeed;
        }
        else if(Input.GetKey(KeyCode.D)) 
        {
            //rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            //pos.position += moveSpeed * Time.deltaTime * Vector3.right;
            horizontalMovement = moveSpeed;
        }

        rb.linearVelocity = new Vector2(horizontalMovement, rb.linearVelocity.y);
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Shoot() 
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Instantiate(bullet, pos.position, bullet.transform.rotation);
        }
    }


    public void Shield() 
    {
        if(Input.GetKeyDown(KeyCode.F) && !isShieldActive && shieldTimer >= maxShieldTime) 
        {
            StartCoroutine(ActivateShield());
            shieldTimer = 0f;
            StartCoroutine(ShieldTimerCount());
        }
    }

    void FreezeX()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;;
    }
    void UnFreezeX()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    System.Collections.IEnumerator ActivateShield()
    {
        // Set the shield as active
        isShieldActive = true;
        shield.SetActive(true);
        FreezeX();

        // Wait for the shield duration
        yield return new WaitForSeconds(shieldDuration);

        // Deactivate the shield after the duration
        shield.SetActive(false);
        isShieldActive = false;
        UnFreezeX();
    }
    
    System.Collections.IEnumerator ShieldTimerCount() 
    {
        while(shieldTimer < maxShieldTime) 
        {
            yield return new WaitForSeconds(1f);
            shieldTimer++;
            timerText.GetComponent<TextMeshProUGUI>().text = "Timer: " + shieldTimer;
        }
        
    }

    public void SPM() 
    {
        if(Input.GetKeyDown(KeyCode.R)&& SPM_timer >= maxSPM_time) 
        {
            GameObject missile = Instantiate(SPM_missile, pos.position, SPM_missile.transform.rotation);
            StartCoroutine(IncreaseMissileScale(missile));
            SPM_timer = 0f;
            StartCoroutine(SPMTimerCount());
        }
    }

    System.Collections.IEnumerator IncreaseMissileScale(GameObject missile) 
    {
        Vector3 initialScale = missile.transform.localScale;
        Vector3 targetScale = new Vector3(maxMissileScale, maxMissileScale, missile.transform.localScale.z);

        float duration = 3f;
        float time = 0f;

        while(time < duration) 
        {
            missile.transform.localScale = Vector3.Lerp(initialScale, targetScale, time/duration);
            time += Time.deltaTime;
            yield return null;
        }

        missile.transform.localScale = targetScale;
    }

    System.Collections.IEnumerator SPMTimerCount() 
    {
        while(SPM_timer < maxSPM_time) 
        {
            yield return new WaitForSeconds(1f);
            SPM_timer++;
        }
        
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) 
        {
            Instantiate(hit_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.CompareTag("Barrier")) 
        {
            Instantiate(hit_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
