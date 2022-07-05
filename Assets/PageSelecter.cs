using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSelecter : MonoBehaviour
{
    private int currentPage;
    private int maxPage;
    public GameObject page;
    // Start is called before the first frame update
    void Start()
    {
        currentPage = 1;
        maxPage = 2;
    }

    IEnumerator MovePage(Vector3 dist, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            page.transform.position = Vector3.Lerp(page.transform.position, page.transform.position + dist, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void NextPage()
    {
        if (currentPage < maxPage)
        {
            StartCoroutine(MovePage(new Vector3(-45, 0, 0), 1));
            currentPage++;
        }
    }
}