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
    public int pickupGoal;
    private float timer;

    [SerializeField]
    private GameObject player;
    private ScoreManager scoreManager;
    private Controller playerController;

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = 0;
        timer = spawnCooldown;
        scoreManager = player.GetComponent<ScoreManager>();
        playerController = player.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCount < spawnLimit && scoreManager.score + spawnCount < pickupGoal)
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
        newPickup.transform.position = new Vector3(Random.Range(-playerController.xLimit, playerController.xLimit),
                                                   0.0f,
                                                   Random.Range(-playerController.zLimit, playerController.zLimit)); // todo make boundary values
        spawnCount++;
    }
}
