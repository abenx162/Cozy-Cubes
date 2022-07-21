using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColour : MonoBehaviour
{
    [SerializeField] private Material myMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = new Color32(192, 28, 28, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
