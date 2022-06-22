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
    private static float skipInput;

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
        skipInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Prevents the skip from working every frame by holding down spacebar
        if (skipInput == 0)
        {
            skipInput = Input.GetAxisRaw("Jump");

            if (skipInput != 0 && pickupsSpawned.Count != spawnLocations.Count())
            {
                timer = 0.0f;
            }
        }
        else
        {
            skipInput = Input.GetAxisRaw("Jump");
        }

        // Letting it run just does it normally
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
