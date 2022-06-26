using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreGUIText; // In game text
    public TextMeshProUGUI defuseGUIText;
    private int pickupGoal;
    private int defuseGoal;
    public int score { get; private set; }
    public int defuses { get; private set; }
    [SerializeField]
    private FinishZone[] finishZone;
    [SerializeField]
    private GameStateControl gameStateControl;
    [SerializeField]
    private TextMeshProUGUI pickupCounter;
    [SerializeField]
    private TextMeshProUGUI trapCounter;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        defuses = 0;
        pickupGoal = 1;
        defuseGoal = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= pickupGoal && defuses >= defuseGoal && !finishZone[gameStateControl.currentLevel].zoneActive)
        {
            finishZone[gameStateControl.currentLevel].EnableZone();
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
            pickupCounter.text = score.ToString();
            scoreGUIText.text = score.ToString() + "/" + pickupGoal;
        }

        // Pickup
        if (other.TryGetComponent(out TrapController tController))
        {
            Controller playerController = GetComponent<Controller>();
            if (playerController.charged == 2)
            {
                tController.transform.Rotate(-playerController.rb.velocity.x * 2.0f, 0.0f, -playerController.rb.velocity.z * 2.0f);
                tController.GetComponent<AudioSource>().Play();
                playerController.UpgradeCharge();
                tController.enabled = false;
                other.enabled = false;

                defuses++;
                trapCounter.text = defuses.ToString();
                defuseGUIText.text = defuses.ToString() + "/" + defuseGoal;
            }
        }
    }

    public void ResetScore()
    {
        score = 0;
        defuses = 0;
        scoreGUIText.text = score.ToString() + "/" + pickupGoal;
        defuseGUIText.text = defuses.ToString() + "/" + defuseGoal;
    }

    public void SetPickupGoal(int goal)
    {
        pickupGoal = goal;
        scoreGUIText.text = score.ToString() + "/" + pickupGoal;
    }

    public void SetDefuseGoal(int goal)
    {
        defuseGoal = goal;
        defuseGUIText.text = defuses.ToString() + "/" + defuseGoal;
    }
}
