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
        GameObject.Find("Wall Cursor").GetComponent<WallCursor>().setActive(true);
        GameObject.Find("Player Cursor").GetComponent<PlayerCursor>().setActive(false);
        GameObject.Find("Square Cursor").GetComponent<SquareCursor>().setActive(false);
    }

    public void PlayerButton()
    {
        GameObject.Find("Wall Cursor").GetComponent<WallCursor>().setActive(false);
        GameObject.Find("Player Cursor").GetComponent<PlayerCursor>().setActive(true);
        GameObject.Find("Square Cursor").GetComponent<SquareCursor>().setActive(false);
    }

    public void SquareButton()
    {
        GameObject.Find("Wall Cursor").GetComponent<WallCursor>().setActive(false);
        GameObject.Find("Player Cursor").GetComponent<PlayerCursor>().setActive(false);
        GameObject.Find("Square Cursor").GetComponent<SquareCursor>().setActive(true);
    }
}
