using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    // Pickup spawn settings
    public GameObject pickup;
    public int spawnCooldown;
    private float timer;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject[] spawnLocations;
    [SerializeField]
    private GameObject[] cameraLocations;
    private List<GameObject> pickupsSpawned;
    [SerializeField]
    private GameStateControl gameStateControl;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnCooldown;
        pickupsSpawned = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupsSpawned.Count < spawnLocations.Count())
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                Camera.main.transform.position = cameraLocations[pickupsSpawned.Count].transform.position;
                Camera.main.transform.rotation = cameraLocations[pickupsSpawned.Count].transform.rotation;
                GeneratePickup();
                timer = spawnCooldown;
            }
        }
        else if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                gameStateControl.SetGameState(GameStateControl.GameState.Playing);
            }
        }
    }

    public void GeneratePickup()
    {
        GameObject newPickup = Instantiate(pickup);
        newPickup.transform.position = spawnLocations[pickupsSpawned.Count].transform.position; // todo make boundary values
        pickupsSpawned.Add(newPickup);
    }
}
