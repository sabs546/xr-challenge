using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private enum TransformType { Move, Rotate, Grow, Shrink };
    private enum TriggerType { Hit, Leave, Constant };
    [SerializeField]
    private TransformType transformType;
    [SerializeField]
    private TriggerType triggerType;
    private bool triggered;
    [SerializeField]
    private bool looping;
    [SerializeField]
    private float xSpeed;
    [SerializeField]
    private float ySpeed;
    [SerializeField]
    private float zSpeed;
    [SerializeField]
    private Vector3 limits;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;

    private float tempX; // For convenience
    private float tempY;
    private float tempZ;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
        triggered = triggerType == TriggerType.Constant ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered)
        {
            return;
        }

        switch (transformType)
        {
            case TransformType.Move:
                MovePlatform();
                break;
            case TransformType.Grow:
                GrowPlatform();
                break;
            case TransformType.Shrink:
                ShrinkPlatform();
                break;
        }
    }

    private void MovePlatform()
    {
        // Set new positions
        tempX = transform.localPosition.x + (xSpeed * Time.deltaTime);
        tempY = transform.localPosition.y + (ySpeed * Time.deltaTime);
        tempZ = transform.localPosition.z + (zSpeed * Time.deltaTime);

        // Stop them from overshooting
        tempX = Mathf.Clamp(tempX, originalPosition.x - limits.x, originalPosition.x + limits.x);
        tempY = Mathf.Clamp(tempY, originalPosition.y - limits.y, originalPosition.y + limits.y);
        tempZ = Mathf.Clamp(tempZ, originalPosition.z - limits.z, originalPosition.z + limits.z);

        // Move the thing
        transform.localPosition = new Vector3(tempX, tempY, tempZ);

        if (looping)
        {
            // Reverse the speed;
            if (tempX == originalPosition.x - limits.x || tempX == originalPosition.x + limits.x)
            {
                xSpeed *= -1.0f;
            }
            if (tempY == originalPosition.y - limits.y || tempY == originalPosition.y + limits.y)
            {
                ySpeed *= -1.0f;
            }
            if (tempZ == originalPosition.z - limits.z || tempZ == originalPosition.z + limits.z)
            {
                zSpeed *= -1.0f;
            }
        }
    }

    private void GrowPlatform()
    {
        tempX = transform.localScale.x + (xSpeed * Time.deltaTime);
        tempY = transform.localScale.y + (ySpeed * Time.deltaTime);
        tempZ = transform.localScale.z + (zSpeed * Time.deltaTime);

        // Stop them from overshooting
        tempX = Mathf.Clamp(tempX, originalScale.x - limits.x, originalScale.x + limits.x);
        tempY = Mathf.Clamp(tempY, originalScale.y - limits.y, originalScale.y + limits.y);
        tempZ = Mathf.Clamp(tempZ, originalScale.z - limits.z, originalScale.z + limits.z);

        // Move the thing
        transform.localScale = new Vector3(tempX, tempY, tempZ);

        if (looping)
        {
            // Reverse the speed;
            if (tempX == originalScale.x || tempX == originalScale.x + limits.x)
            {
                xSpeed *= -1.0f;
            }
            if (tempY == originalScale.y || tempY == originalScale.y + limits.y)
            {
                ySpeed *= -1.0f;
            }
            if (tempZ == originalScale.z || tempZ == originalScale.z + limits.z)
            {
                zSpeed *= -1.0f;
            }
        }
    }

    private void ShrinkPlatform()
    {
        tempX = transform.localScale.x - (xSpeed * Time.deltaTime);
        tempY = transform.localScale.y - (ySpeed * Time.deltaTime);
        tempZ = transform.localScale.z - (zSpeed * Time.deltaTime);

        // Stop them from overshooting
        tempX = Mathf.Clamp(tempX, originalScale.x - limits.x, originalScale.x);
        tempY = Mathf.Clamp(tempY, originalScale.y - limits.y, originalScale.y);
        tempZ = Mathf.Clamp(tempZ, originalScale.z - limits.z, originalScale.z);

        // Move the thing
        transform.localScale = new Vector3(tempX, tempY, tempZ);

        if (looping)
        {
            // Reverse the speed;
            if (tempX == originalScale.x || tempX == originalScale.x - limits.x)
            {
                xSpeed *= -1.0f;
            }
            if (tempY == originalScale.y || tempY == originalScale.y - limits.y)
            {
                ySpeed *= -1.0f;
            }
            if (tempZ == originalScale.z || tempZ == originalScale.z - limits.z)
            {
                zSpeed *= -1.0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (triggerType == TriggerType.Hit)
        {
            triggered = true;
        }
        else if (triggerType == TriggerType.Leave)
        {
            triggered = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (triggerType == TriggerType.Leave)
        {
            triggered = true;
        }
        else if (triggerType == TriggerType.Hit)
        {
            triggered = false;
        }
    }
}
