using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
 
public class RestartLevel : MonoBehaviour
{
    public GameObject WinTxt;

    void Update()
    {
        if (Input.GetKey("r")) {
            Restart();
        }

        if (AllClear()) {
            WinTxt.SetActive(true);
        }
    }
 
    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool AllClear() {
        return GameObject.Find("Square (2)").GetComponent<ChangeColour>().Done() &&
                GameObject.Find("Square (3)").GetComponent<ChangeColour>().Done() &&
                GameObject.Find("Square (4)").GetComponent<ChangeColour>().Done();
    }
}
