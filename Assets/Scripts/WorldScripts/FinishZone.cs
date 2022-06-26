using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    public bool zoneActive { get; private set; }
    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;
    [SerializeField]
    private GameObject redSmoke;
    [SerializeField]
    private GameObject greenSmoke;
    public MeshRenderer meshRenderer { get; private set; }
    private AudioSource source;
    [SerializeField]
    private GameStateControl gameStateControl;

    // Start is called before the first frame update
    void Start()
    {
        zoneActive = false;
        meshRenderer = GetComponent<MeshRenderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameStateControl.SetGameState(GameStateControl.GameState.LevelComplete);
    }

    public void EnableZone()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = green;
        redSmoke.SetActive(false);
        greenSmoke.SetActive(true);
        source.Play();
        GetComponent<CapsuleCollider>().enabled = true;
        zoneActive = true;
    }
}
