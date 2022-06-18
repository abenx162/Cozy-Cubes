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

    public void GoToLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void GoToLevel4()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void GoToLevel5()
    {
        SceneManager.LoadScene("Level 5");
    }

    public void GoToLevel6()
    {
        SceneManager.LoadScene("Level 6");
    }

    public void GoToLevel7()
    {
        SceneManager.LoadScene("Level 7");
    }

    public void GoToLevel8()
    {
        SceneManager.LoadScene("Level 8");
    }

    public void GoToLevel9()
    {
        SceneManager.LoadScene("Level 9");
    }

    public void GoToLevel10()
    {
        SceneManager.LoadScene("Level 10");
    }

    

}
