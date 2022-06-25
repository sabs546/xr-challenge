using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCommand : MonoBehaviour
{
    [SerializeField]
    private float pelletSpeed;
    [SerializeField]
    private float pelletLifetime;
    [SerializeField]
    private GameObject pellet;
    private GameObject pelletInstance;
    private float timer;
    private bool pelletActive;

    // Start is called before the first frame update
    void Start()
    {
        timer = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            pelletInstance.transform.localPosition += new Vector3(pellet.transform.localPosition.x, pellet.transform.localPosition.y + (pelletSpeed * Time.deltaTime), pellet.transform.localPosition.z);
        }
        else
        {
            Destroy(pelletInstance);
            pelletActive = false;
        }
    }

    public void SpawnPellet()
    {
        if (!pelletActive)
        {
            pelletInstance = Instantiate(pellet, transform);
            timer = pelletLifetime;
            pelletActive = true;
        }
    }
}
