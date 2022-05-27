using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelecter : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("r")) {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

}
