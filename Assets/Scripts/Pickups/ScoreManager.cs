using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI GUIText; // Score text
    public PickupSpawner spawner;   // To lower the spawn count
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Object
        Pickup pickup = other.gameObject.GetComponent<Pickup>();
        pickup.GetPickedUp();
        Destroy(pickup.gameObject);
        spawner.spawnCount--;

        // Score
        score++;
        GUIText.text = score.ToString();
    }
}
