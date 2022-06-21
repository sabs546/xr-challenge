using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed;
    private Controls controls;
    private Vector3 pos;

    public float xLimit;
    public float zLimit;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        controls = GetComponent<Controls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(controls.Up) && pos.x < xLimit)
        {
            pos = new Vector3(pos.x + (speed * Time.deltaTime), pos.y, pos.z);
        }
        if (Input.GetKey(controls.Down) && pos.x > -xLimit)
        {
            pos = new Vector3(pos.x - (speed * Time.deltaTime), pos.y, pos.z);
        }
        if (Input.GetKey(controls.Left) && pos.z < zLimit)
        {
            pos = new Vector3(pos.x, pos.y, pos.z + (speed * Time.deltaTime));
        }
        if (Input.GetKey(controls.Right) && pos.z > -zLimit)
        {
            pos = new Vector3(pos.x, pos.y, pos.z - (speed * Time.deltaTime));
        }
        transform.position = pos;
    }
}
