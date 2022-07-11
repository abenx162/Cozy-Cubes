using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
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
        if (Input.GetKeyDown("r")) {
            Restart();
        }
    }

    public void Restart()
    {
        IEnumerator Rstrt()
        {
            transition.SetTrigger("EndScene");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        StartCoroutine(Rstrt());
    }
}
