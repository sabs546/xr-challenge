using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBobbing : MonoBehaviour
{
    private float timer; // For footstep spacing
    private float currentYBounce;

    [SerializeField]
    private Controller playerController;
    [SerializeField]
    private AudioClip[] footStepsRocky;
    [SerializeField]
    private AudioClip[] footStepsClean;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        source = playerController.audioSource[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerController.moving && playerController.Ground != playerController.Air)
        {
            timer += playerController.currentSpeedLimit * Time.deltaTime;
            if (transform.localPosition.y < 0.5f)
            {
                // todo need a floor value
                transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
                currentYBounce = playerController.bouncePower / playerController.charged;
                if (playerController.Ground == playerController.RockyGround)
                {
                    source.clip = footStepsRocky[Random.Range(0, footStepsRocky.Length)];
                    source.Play();
                }
                else if (playerController.Ground == playerController.CleanGround)
                {
                    source.clip = footStepsClean[Random.Range(0, footStepsClean.Length)];
                    source.Play();
                }
            }
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + currentYBounce, transform.localPosition.z);
            currentYBounce -= playerController.fallSpeed * Time.deltaTime;
        }
    }
}
