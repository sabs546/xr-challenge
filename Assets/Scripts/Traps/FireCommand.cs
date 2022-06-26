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
    private AudioSource source;
    private TrapController controller;

    // Start is called before the first frame update
    void Start()
    {
        timer = -1.0f;
        if (TryGetComponent(out AudioSource src))
        {
            source = src;
        }
        controller = GetComponentInParent<TrapController>();
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
        if (!pelletActive && GameStateControl.gameState == GameStateControl.GameState.Playing)
        {
            if (source != null)
            {
                source.volume = 1 - (controller.GetDistance() * 0.1f);
                source.Play();
            }

            pelletInstance = Instantiate(pellet, transform);
            timer = pelletLifetime;
            pelletActive = true;
            pelletInstance.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
