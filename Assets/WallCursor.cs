using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCursor : MonoBehaviour
{
    private bool isActive;
    private Transform _transform;
    public GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        isActive = false;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos = new Vector3(Mathf.Round(pos.x + 0.5f) - 0.5f, Mathf.Round(pos.y + 0.5f) - 0.5f, 0);

        if (isActive && pos.y <= 4 && pos.y >= -4 && pos.x <= 6 && pos.x >= -7)
        {
            _transform.position = Vector2.Lerp(_transform.position, pos, 100);
        }
    }

    public void setActive(bool tf)
    {
        isActive = tf;
        if (tf) 
        {
            GameObject.Find("Block Drawer").GetComponent<BlockDrawer>().setActiveBlock(block);
        } else 
        {
            _transform.position += new Vector3(Screen.width, 0, 0);
        }
    }
}
