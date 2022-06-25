using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneratePlatform : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private bool triggered;

    [SerializeField]
    private float xMoveSpeed;
    [SerializeField]
    private float yMoveSpeed;
    [SerializeField]
    private float zMoveSpeed;

    [SerializeField]
    private float xGrowSpeed;
    [SerializeField]
    private float yGrowSpeed;
    [SerializeField]
    private float zGrowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            float tempPosX = Mathf.Clamp(transform.position.x, transform.position.x, originalPosition.x);
            float tempPosY = Mathf.Clamp(transform.position.y, transform.position.y, originalPosition.y);
            float tempPosZ = Mathf.Clamp(transform.position.z, transform.position.z, originalPosition.z);

            float tempScaleX = Mathf.Clamp(transform.localScale.x, transform.localScale.x, originalScale.x);
            float tempScaleY = Mathf.Clamp(transform.localScale.y, transform.localScale.y, originalScale.y);
            float tempScaleZ = Mathf.Clamp(transform.localScale.z, transform.localScale.z, originalScale.z);

            transform.position = new Vector3(tempPosX, tempPosY, tempPosZ);
            transform.localScale = new Vector3(tempScaleX, tempScaleY, tempScaleZ);

            if (transform.position != originalPosition)
            {
                transform.position = new Vector3(transform.position.x + (xMoveSpeed * Time.deltaTime), transform.position.y + (yMoveSpeed * Time.deltaTime), transform.position.z + (zMoveSpeed * Time.deltaTime));
            }
            else
            {
                transform.position = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
            }

            if (transform.localScale != originalScale)
            {
                transform.localScale = new Vector3(transform.localScale.x + (xGrowSpeed * Time.deltaTime), transform.localScale.y + (yGrowSpeed * Time.deltaTime), transform.localScale.z + (zGrowSpeed * Time.deltaTime));
            }
            else
            {
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }

            if (transform.position == originalPosition && transform.localScale == originalScale)
            {
                triggered = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        triggered = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        triggered = false;
    }
}
