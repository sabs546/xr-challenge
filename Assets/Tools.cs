using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public Controller playerController;
    public ScoreManager scoreManager;
    public GameObject[] pickupLocations1;
    public GameObject[] pickupLocations2;
    public GameStateControl gameStateControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gameStateControl.currentLevel == 0 && scoreManager.score < pickupLocations1.Length)
            {
                playerController.transform.position = pickupLocations1[scoreManager.score].transform.position;
            }
            else if (gameStateControl.currentLevel == 1 && scoreManager.score < pickupLocations2.Length)
            {
                playerController.transform.position = pickupLocations2[scoreManager.score].transform.position;
            }
        }
    }
}
