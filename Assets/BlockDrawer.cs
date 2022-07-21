using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDrawer : MonoBehaviour
{
    private GameObject activeBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos = new Vector3(Mathf.Round(pos.x + 0.5f) - 0.5f, Mathf.Round(pos.y + 0.5f) - 0.5f, 0);

        if (Input.GetMouseButtonDown(0) && activeBlock != null && pos.y <= 4 && pos.y >= -4 && pos.x <= 6 && pos.x >= -7)
        {
            if (activeBlock == GameObject.Find("Player"))
            {
                activeBlock.transform.position = pos;
                activeBlock.transform.SetParent(GameObject.Find("Placed Blocks").transform);
            } else
            {
                Instantiate(activeBlock, pos, Quaternion.identity, GameObject.Find("Placed Blocks").transform);
            }
        }
    }

    public void setActiveBlock(GameObject block)
    {
        activeBlock = block;
    }
}
