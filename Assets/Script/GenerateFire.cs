using UnityEngine;

public class GenerateFire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject boltPrefab;
    public GameObject pulsePrelab;
    public GameObject chargedPrefab;
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
        int n = Random.Range(1, 4);
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
        else
        {
            GameObject fire = Instantiate(pulsePrelab);
            fire.transform.position = new Vector3(13, Random.Range(-4, 4), 0);
        }
    }
}
