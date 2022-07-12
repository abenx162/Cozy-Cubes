using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageButtons : MonoBehaviour
{
    public GameObject pages;
    private Vector3 defaultPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SavePos()
    {
        defaultPos = transform.position;
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
        transform.position = defaultPos + new Vector3(Screen.width, 0, 0);
    }

    public void Show()
    {
        transform.position = defaultPos;
    }
}
