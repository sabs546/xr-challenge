using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI GUIText; // Score text
    public int pickupGoal;
    public int score { get; private set; }
    [SerializeField]
    private FinishZone finishZone;
    [SerializeField]
    private GameStateControl gameStateControl;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= pickupGoal && !finishZone.zoneActive)
        {
            finishZone.EnableZone();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pickup
        if (other.TryGetComponent(out Pickup pickup))
        {
            // Object
            pickup.GetPickedUp();
            pickup.GetComponent<CapsuleCollider>().enabled = false;

            // Audio
            pickup.GetComponent<AudioSource>().Play();

            // Score
            score++;
            GUIText.text = score.ToString();
        }
        else if (score >= pickupGoal)
        {
            gameStateControl.SetGameState(GameStateControl.GameState.LevelComplete);
        }
    }

    public void ResetScore()
    {
        score = 0;
    }
}
