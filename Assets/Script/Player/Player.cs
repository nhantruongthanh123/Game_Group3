using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PLayer : MonoBehaviour
{
    [SerializeField] private GameObject myBody;
    private float jumpForce = 8f;
    private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform pos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject SPM_missile;
    [SerializeField] private GameObject hit_effect;
    [SerializeField] private GameObject explosion_effect;
    [SerializeField] private GameObject smallShield;
    //[SerializeField] private Collider2D myCollider;
    private float smallShieldDuration = 2f;
    private float shieldDuration = 5f;
    public bool isShieldActive = false;
    private bool isShieldBreak = false;
    private bool isSMShieldActive = false;
    private float shieldTimer = 0f;
    private float maxShieldTime = 15f;
    private float maxMissileScale = 40f;
    private float SPM_timer = 0f;
    private float maxSPM_time = 30f;
    private float shootCooldown = 1f;
    private float shootTimer = 0f;




    // Start is called before the first frame update
    void Start()
    {
        rb = myBody.GetComponent<Rigidbody2D>();
        pos = myBody.GetComponent<Transform>();
        shield.SetActive(false);
        smallShield.SetActive(false);
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
        if (shootTimer < shootCooldown)
        {
            shootTimer += Time.deltaTime;
        }
    }

    public void Move() 
    {
        float horizontalMovement = 0f;
        if(Input.GetKey(KeyCode.A)) 
        {
            horizontalMovement = -moveSpeed;
        }
        else if(Input.GetKey(KeyCode.D)) 
        {
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
        if(Input.GetKeyDown(KeyCode.Mouse0) && shootTimer >= shootCooldown) 
        {
            Instantiate(bullet, pos.position, bullet.transform.rotation);
            shootTimer = 0f;
        }
    }


    public void Shield() 
    {
        if(Input.GetKeyDown(KeyCode.F) && !isShieldActive && shieldTimer >= maxShieldTime) 
        {
            StartCoroutine(ActivateShield());
        }
    }

    System.Collections.IEnumerator ActivateShield()
    {
        if (isShieldBreak) 
        {
            yield break; // Nếu đang trong thời gian hồi hoặc đã va chạm, không kích hoạt shield
        }
        
        // Set the shield as active
        isShieldActive = true;
        shield.SetActive(true);

        // Wait for the shield duration
        yield return new WaitForSeconds(shieldDuration);

        // Deactivate the shield after the duration
        shield.SetActive(false);
        isShieldActive = false;

        shieldTimer = 0f;
        StartCoroutine(ShieldTimerCount());
        
        
    }
    
    System.Collections.IEnumerator ShieldTimerCount() 
    {
        while(shieldTimer < maxShieldTime) 
        {
            yield return new WaitForSeconds(1f);
            shieldTimer++;
        }
        isShieldBreak = false; // Reset shield break status after cooldown
        
    }

    System.Collections.IEnumerator ActivateSmallShield() {
        smallShield.SetActive(true);
        isSMShieldActive = true;
        yield return new WaitForSeconds(smallShieldDuration);
        smallShield.SetActive(false);
        isSMShieldActive = false;
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

        float duration = 2f;
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
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Asteroid")) 
        {
            
            if(!isShieldActive) 
            {
                Instantiate(explosion_effect, transform.position, Quaternion.identity);
                Destroy(gameObject);
                GameManager.Instance.isGameOver = true;
            }
            else 
            {
                shield.SetActive(false);
                isShieldActive = false;
                isShieldBreak = true;
                shieldTimer = 0f;
                StartCoroutine(ShieldTimerCount());
                Instantiate(explosion_effect, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.CompareTag("Barrier")) 
        {
            Instantiate(explosion_effect, transform.position, Quaternion.identity);
            if(!isShieldActive) 
            {
                if(isSMShieldActive) 
                {
                    smallShield.SetActive(false);
                    isSMShieldActive = false;
                }
                else 
                {
                    Destroy(gameObject);
                    GameManager.Instance.isGameOver = true;
                }
            }
            else 
            {
                shield.SetActive(false);
                isShieldActive = false;
                isShieldBreak = true;
                shieldTimer = 0f;
                StartCoroutine(ShieldTimerCount());
                StartCoroutine(ActivateSmallShield());
            }
        }
    }
}
