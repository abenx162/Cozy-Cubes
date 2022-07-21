using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        // GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("PlacedBlock");
        foreach (Transform item in GameObject.Find("Placed Blocks").transform)
        {
            MonoBehaviour[] scripts = item.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }
        }

        GameObject.Find("End Test").transform.position = gameObject.transform.position;
        gameObject.transform.position += new Vector3(1000, 0, 0);
    }
}
