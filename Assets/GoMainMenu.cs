using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoMainMenu : MonoBehaviour
{
    private Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        transition = GameObject.Find("CubeZoom").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        IEnumerator GoMenu()
        {
            transition.SetTrigger("EndScene");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Main Menu");
        }

        StartCoroutine(GoMenu());
        
    }
}
