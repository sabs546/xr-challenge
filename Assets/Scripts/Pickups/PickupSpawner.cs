using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    // Pickup spawn settings
    public GameObject pickup;
    public int spawnLimit;
    public int spawnCooldown;
    public int spawnCount;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = 0;
        timer = spawnCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCount < spawnLimit)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                GeneratePickup();
                timer = spawnCooldown;
            }
        }
    }

    public void GeneratePickup()
    {
        GameObject newPickup = Instantiate(pickup);
        newPickup.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f)); // todo make boundary values
        spawnCount++;
    }
}
