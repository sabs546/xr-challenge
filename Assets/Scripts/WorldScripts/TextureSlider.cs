using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TextureSlider : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer[] mesh;
    [SerializeField]
    private float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < mesh.Length; i++)
        {
            float offset = Time.time * scrollSpeed;
            mesh[i].material.SetTextureOffset("_MainTex", new Vector2(offset, offset * 0.5f));
        }
    }
}
