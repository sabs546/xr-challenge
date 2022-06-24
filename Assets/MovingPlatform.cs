using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float xSpeed;
    [SerializeField]
    private float ySpeed;
    [SerializeField]
    private float zSpeed;
    [SerializeField]
    private bool alwaysMoving;
    [SerializeField]
    private Vector3 movementLimits;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private float tempX; // For convenience
    private float tempY;
    private float tempZ;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Set new positions
        tempX = transform.position.x + (xSpeed * Time.deltaTime);
        tempY = transform.position.y + (ySpeed * Time.deltaTime);
        tempZ = transform.position.z + (zSpeed * Time.deltaTime);

        // Stop them from overshooting
        tempX = Mathf.Clamp(tempX, originalPosition.x - movementLimits.x, originalPosition.x + movementLimits.x);
        tempY = Mathf.Clamp(tempY, originalPosition.y - movementLimits.y, originalPosition.y + movementLimits.y);
        tempZ = Mathf.Clamp(tempZ, originalPosition.z - movementLimits.z, originalPosition.z + movementLimits.z);

        // Move the thing
        transform.position = new Vector3(tempX, tempY, tempZ);

        // Reverse the speed;
        if (tempX == originalPosition.x - movementLimits.x || tempX == originalPosition.x + movementLimits.x)
        {
            xSpeed *= -1.0f;
        }
        if (tempY == originalPosition.y - movementLimits.y || tempY == originalPosition.y + movementLimits.y)
        {
            ySpeed *= -1.0f;
        }
        if (tempZ == originalPosition.z - movementLimits.z || tempZ == originalPosition.z + movementLimits.z)
        {
            zSpeed *= -1.0f;
        }
    }
}
