using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotateSpeed;
    [SerializeField]
    private Vector3 rotateLimits;
    private Quaternion originalOrientation;
    [SerializeField]
    private float triggerRange;
    private float distance;
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private Transform orientation;
    [SerializeField]
    private Transform cannonHolder;
    [SerializeField]
    private Controller playerController;
    [SerializeField]
    private FireCommand[] cannons;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        originalOrientation = transform.localRotation;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float xDist = orientation.position.x - playerPos.position.x;
        float yDist = orientation.position.y - playerPos.position.y;
        float zDist = orientation.position.z - playerPos.position.z;

        distance = (xDist * xDist) + (yDist * yDist) + (zDist * zDist);
        distance = Mathf.Sqrt(distance);

        if (distance <= triggerRange)
        {
            orientation.LookAt(playerPos);
            if (cannonHolder.localRotation.x < orientation.localRotation.x && cannonHolder.localRotation.x > originalOrientation.x - rotateLimits.x)
            {
                cannonHolder.Rotate(Vector3.right, rotateSpeed.x * Time.deltaTime);
            }
            else if (cannonHolder.localRotation.x > orientation.localRotation.x && cannonHolder.localRotation.x < originalOrientation.x + rotateLimits.x)
            {
                cannonHolder.Rotate(Vector3.left, rotateSpeed.x * Time.deltaTime);
            }
            if (cannonHolder.localRotation.y < orientation.localRotation.y && cannonHolder.localRotation.y > originalOrientation.y - rotateLimits.y)
            {
                cannonHolder.Rotate(Vector3.up, rotateSpeed.y * Time.deltaTime);
            }
            else if (cannonHolder.localRotation.y > orientation.localRotation.y && cannonHolder.localRotation.y < originalOrientation.y + rotateLimits.y)
            {
                cannonHolder.Rotate(Vector3.down, rotateSpeed.y * Time.deltaTime);
            }
            for (int i = 0; i < cannons.Length; i++)
            {
                cannons[i].SpawnPellet();
            }
        }
    }

    public float GetDistance()
    {
        return distance;
    }
}
