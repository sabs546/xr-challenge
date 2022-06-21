using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField]
    private bool zoneActive;
    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        zoneActive = false;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void EnableZone()
    {
        meshRenderer.material = green;
        GetComponent<CapsuleCollider>().enabled = true;
    }
}
