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
    public MeshRenderer meshRenderer { get; private set; }

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
        zoneActive = true;
    }
}
