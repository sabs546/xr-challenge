using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField]
    private GameObject target;
    private Vector3 targetPos;

    [SerializeField]
    private float xLimit;
    [SerializeField]
    private float zLimit;

    // Start is called before the first frame update
    void Start()
    {
        pos = target.transform.position;
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = target.transform.position;
        pos = targetPos;
        if (pos.x > xLimit)
        {
            pos.x = xLimit;
        }
        else if (pos.x < -xLimit)
        {
            pos.x = -xLimit;
        }
        if (pos.z > zLimit)
        {
            pos.z = zLimit;
        }
        else if (pos.z < -zLimit)
        {
            pos.z = -zLimit;
        }
        transform.position = pos;
    }
}
