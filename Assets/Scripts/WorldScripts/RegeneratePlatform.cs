using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneratePlatform : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 triggerPosition;
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
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            // todo this will probably backfire thanks to the magic of negative co-ordinates so maybe I'll fix it when I have the time
            float tempPosX = Mathf.Clamp(transform.localPosition.x, Mathf.Min(triggerPosition.x, originalPosition.x), Mathf.Max(triggerPosition.x, originalPosition.x));
            float tempPosY = Mathf.Clamp(transform.localPosition.y, Mathf.Min(triggerPosition.y, originalPosition.y), Mathf.Max(triggerPosition.y, originalPosition.y));
            float tempPosZ = Mathf.Clamp(transform.localPosition.z, Mathf.Min(triggerPosition.z, originalPosition.z), Mathf.Max(triggerPosition.z, originalPosition.z));

            // I don't think the low end matters here since we're just going up anyway
            float tempScaleX = Mathf.Clamp(transform.localScale.x, 0.0f, originalScale.x);
            float tempScaleY = Mathf.Clamp(transform.localScale.y, 0.0f, originalScale.y);
            float tempScaleZ = Mathf.Clamp(transform.localScale.z, 0.0f, originalScale.z);

            transform.position = new Vector3(tempPosX, tempPosY, tempPosZ);
            transform.localScale = new Vector3(tempScaleX, tempScaleY, tempScaleZ);

            if (transform.position != originalPosition)
            {
                transform.position = new Vector3(transform.localPosition.x + (xMoveSpeed * Time.deltaTime), transform.localPosition.y + (yMoveSpeed * Time.deltaTime), transform.localPosition.z + (zMoveSpeed * Time.deltaTime));
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

            if (transform.localPosition == originalPosition && transform.localScale == originalScale)
            {
                triggered = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        triggered = true;
        triggerPosition = transform.localPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        triggered = false;
    }
}
