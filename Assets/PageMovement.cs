using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageMovement : MonoBehaviour
{

    private int currentPage = 1;
    private int maxPage;

    // Start is called before the first frame update
    void Start()
    {
        maxPage = transform.childCount;
        GameObject.Find("Prev Page").GetComponent<PageButtons>().Hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MovePage(Vector3 start, Vector3 end, float seconds)
    {
        GameObject.Find("Prev Page").GetComponent<Button>().interactable = false;
        GameObject.Find("Next Page").GetComponent<Button>().interactable = false;

        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(start, end, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        GameObject.Find("Prev Page").GetComponent<Button>().interactable = true;
        GameObject.Find("Next Page").GetComponent<Button>().interactable = true;
    }

    public void NextPage()
    {
        if (currentPage < maxPage)
        {
            Vector3 start = transform.position;
            Vector3 end = start + new Vector3(-Screen.width, 0, 0);
            StartCoroutine(MovePage(start, end, 1));
            currentPage++;

            if (currentPage == maxPage)
            {
                GameObject.Find("Next Page").GetComponent<PageButtons>().Hide();
            }

            if (currentPage == 2)
            {
                GameObject.Find("Prev Page").GetComponent<PageButtons>().Show();
            }
        }
    }

    public void PrevPage()
    {
        if (currentPage > 1)
        {
            Vector3 start = transform.position;
            Vector3 end = start + new Vector3(Screen.width, 0, 0);
            StartCoroutine(MovePage(start, end, 1));
            currentPage--;
        }

        if (currentPage == 1)
        {
            GameObject.Find("Prev Page").GetComponent<PageButtons>().Hide();
        }

        if (currentPage == maxPage - 1)
        {
            GameObject.Find("Next Page").GetComponent<PageButtons>().Show();
        }
    }
}
