using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelecter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
