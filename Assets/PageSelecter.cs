using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSelecter : MonoBehaviour
{
    public GameObject pages;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Prev Page").transform.position += new Vector3(Screen.width / 2, 0, 0);
    }

    IEnumerator MovePage(Vector3 start, Vector3 end, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            pages.transform.position = Vector3.Lerp(start, end, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void NextPage()
    {
        if (pages.GetComponent<PageCount>().currentPage < pages.GetComponent<PageCount>().maxPage)
        {
            Vector3 start = pages.transform.position;
            Vector3 end = start + new Vector3(-Screen.width, 0, 0);
            StartCoroutine(MovePage(start, end, 1));
            pages.GetComponent<PageCount>().currentPage++;

            if (pages.GetComponent<PageCount>().currentPage == pages.GetComponent<PageCount>().maxPage)
            {
                gameObject.transform.position += new Vector3(Screen.width, 0, 0);
            }

            if (pages.GetComponent<PageCount>().currentPage == 2)
            {
                GameObject.Find("Prev Page").transform.position -= new Vector3(Screen.width, 0, 0);
            }
        }
    }

    public void PrevPage()
    {
        if (pages.GetComponent<PageCount>().currentPage > 1)
        {
            Vector3 start = pages.transform.position;
            Vector3 end = start + new Vector3(Screen.width, 0, 0);
            StartCoroutine(MovePage(start, end, 1));
            pages.GetComponent<PageCount>().currentPage--;
        }

        if (pages.GetComponent<PageCount>().currentPage == 1)
        {
            gameObject.transform.position += new Vector3(Screen.width, 0, 0);
        }

        if (pages.GetComponent<PageCount>().currentPage == pages.GetComponent<PageCount>().maxPage - 1)
        {
            GameObject.Find("Next Page").transform.position -= new Vector3(Screen.width, 0, 0);
        }
    }
}