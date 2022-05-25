using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
 
public class WinConCheck : MonoBehaviour
{
    public GameObject WinTxt;

    void Update()
    {
        if (AllClear()) {
            WinTxt.SetActive(true);
        }
    }

    private bool AllClear() {
        return GameObject.Find("Square (2)").GetComponent<ChangeColour>().Done() &&
                GameObject.Find("Square (3)").GetComponent<ChangeColour>().Done() &&
                GameObject.Find("Square (4)").GetComponent<ChangeColour>().Done();
    }
}
