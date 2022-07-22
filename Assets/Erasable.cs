using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erasable : MonoBehaviour
{
    private Transform _transform;
    private Vector3 eraserPos;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        eraserPos = GameObject.Find("Eraser Cursor").GetComponent<Transform>().position;
        if (Input.GetMouseButtonDown(0) && _transform.position == eraserPos)
        {
            if (gameObject == GameObject.Find("Player"))
            {
                _transform.position += new Vector3(1000, 0, 0);

            } else
            {
                Destroy(gameObject);
            }
        }

        if (Input.GetMouseButtonDown(0) && IsCollidingBlock() && IsCollidingCursor())
        {
            if (gameObject == GameObject.Find("Player"))
            {
                _transform.position += new Vector3(1000, 0, 0);

            } else
            {
                Destroy(gameObject);
            }
        }
    }

    private bool IsCollidingBlock() {
        return gameObject.ToString() == "Wall Block(Clone) (UnityEngine.GameObject)" || gameObject.ToString() == "Square Block(Clone) (UnityEngine.GameObject)"
            || gameObject == GameObject.Find("Player");
    }

    private bool IsCollidingCursor() {
        return _transform.position == GameObject.Find("Wall Cursor").GetComponent<Transform>().position
            || _transform.position == GameObject.Find("Player Cursor").GetComponent<Transform>().position
            || _transform.position == GameObject.Find("Square Cursor").GetComponent<Transform>().position;
    }
}
