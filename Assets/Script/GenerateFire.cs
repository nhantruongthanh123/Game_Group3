using UnityEngine;

public class GenerateFire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject boltPrefab;
    public GameObject pulsePrelab;
    public GameObject chargedPrefab;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject enemy4;
    [SerializeField] private GameObject enemy5;
    [SerializeField] private GameObject enemy6;
    void Start()
    {
        InvokeRepeating("spawnFire", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void spawnFire()
    {
        int n = Random.Range(1, 10);
        if (n == 1)
        {
            GameObject fire = Instantiate(boltPrefab);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
        else if (n == 2)
        {
            GameObject fire = Instantiate(chargedPrefab);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
        else if (n == 3)
        {
            GameObject fire = Instantiate(pulsePrelab);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
        else if (n == 4) 
        {
            GameObject fire = Instantiate(enemy1);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
        else if (n == 5) 
        {
            GameObject fire = Instantiate(enemy2);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
        else if (n == 6) 
        {
            GameObject fire = Instantiate(enemy3);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
        else if (n == 7) 
        {
            GameObject fire = Instantiate(enemy4);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
        else if (n == 8) 
        {
            GameObject fire = Instantiate(enemy5);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
        else 
        {
            GameObject fire = Instantiate(enemy6);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
    }
}
