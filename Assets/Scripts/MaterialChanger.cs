using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField]
    private FinishZone finishZone;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (finishZone.gameObject.activeSelf && meshRenderer.material.color != finishZone.meshRenderer.material.color)
        {
            meshRenderer.material = finishZone.meshRenderer.material;
        }
    }
}
