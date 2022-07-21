using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColour : MonoBehaviour
{
    [SerializeField] private Material myMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = new Color32(0, 178, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
