using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector2 rotation;
    [SerializeField]
    private Vector2 turnSpeed;
    public Transform orientation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * turnSpeed.x;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * turnSpeed.y;
        rotation.y += mouseX;
        rotation.x -= mouseY;

        rotation.x = Mathf.Clamp(rotation.x, -60.0f, 60.0f);
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        orientation.rotation = Quaternion.Euler(0, rotation.y, 0);
    }
}
