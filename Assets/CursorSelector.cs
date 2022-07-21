using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WallButton()
    {
        StopAllCursors();
        GameObject.Find("Wall Cursor").GetComponent<Cursor>().setActive(true);
    }

    public void PlayerButton()
    {
        StopAllCursors();
        GameObject.Find("Player Cursor").GetComponent<Cursor>().setActive(true);
    }

    public void SquareButton()
    {
        StopAllCursors();
        GameObject.Find("Square Cursor").GetComponent<Cursor>().setActive(true);
    }

    public void CircleButton()
    {
        StopAllCursors();
        GameObject.Find("Circle Cursor").GetComponent<Cursor>().setActive(true);
    }

    public void BricksButton()
    {
        StopAllCursors();
        GameObject.Find("Bricks Cursor").GetComponent<Cursor>().setActive(true);
    }

    public void StopAllCursors()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Cursor");
        foreach (GameObject item in taggedObjects)
        {
            item.GetComponent<Cursor>().setActive(false);
        }
    }
}
