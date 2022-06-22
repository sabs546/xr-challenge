using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCommand : MonoBehaviour
{
    public Vector3 cameraSpeed;
    public Vector3 cameraRotation;
    public Transform pivot;
    public Vector3 pivotPos;
    public Transform cameraLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Relocate one pivot point instead of making multiple
        if (pivot.transform.position != pivotPos)
        {
            pivot.transform.position = pivotPos;
        }

        cameraLocation.transform.RotateAround(pivot.position, Vector3.left, cameraRotation.x * Time.deltaTime);
        cameraLocation.transform.RotateAround(pivot.position, Vector3.up, cameraRotation.y * Time.deltaTime);
        cameraLocation.transform.RotateAround(pivot.position, Vector3.forward, cameraRotation.z * Time.deltaTime);

        Camera.main.transform.position = cameraLocation.position;
        Camera.main.transform.rotation = cameraLocation.rotation;
    }
}
