using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
 
public class WinConCheck : MonoBehaviour
{
    public GameObject WinTxt;

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Movement>().IsStationary() && AllClear()) {
            WinTxt.SetActive(true);
        }
    }

    private bool AllClear() {
        bool clear = true;
        Object[] blocks = Object.FindObjectsOfType<ChangeColour>();
        foreach (Object item in blocks)
        {
            ChangeColour script = (ChangeColour) item;
            clear = clear && script.Done();
        }
        return clear;
    }
}
