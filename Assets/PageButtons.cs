using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageButtons : MonoBehaviour
{
    public GameObject pages;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Next()
    {
        pages.GetComponent<PageMovement>().NextPage();
    }

    public void Prev()
    {
        pages.GetComponent<PageMovement>().PrevPage();
    }

    public void Hide()
    {
        transform.position += new Vector3(Screen.width, 0, 0);
    }

    public void Show()
    {
        transform.position -= new Vector3(Screen.width, 0, 0);
    }
}
