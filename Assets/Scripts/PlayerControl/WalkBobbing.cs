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
    private AudioClip[] footSteps;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerController.moving)
        {
            timer += playerController.currentSpeed * Time.deltaTime;
            if (transform.localPosition.y < 0.5f)
            {
                // todo need a floor value
                transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
                currentYBounce = playerController.bouncePower;
                playerController.audioSource.clip = footSteps[Random.Range(0, footSteps.Length)];
                playerController.audioSource.Play();
            }
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + currentYBounce, transform.localPosition.z);
            currentYBounce -= playerController.fallSpeed * Time.deltaTime;
        }
    }
}
