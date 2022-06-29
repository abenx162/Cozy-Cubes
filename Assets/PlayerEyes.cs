using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    private Transform _transform;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Movement>().IsStationary()) {
            _transform.position = player.GetComponent<Transform>().position + new Vector3(0, 0.1f, 0);
        } else {
            string playerDir = player.GetComponent<Movement>().GetPlayerDir();
            switch (playerDir)
            {
                case "left":
                    _transform.position = player.GetComponent<Transform>().position + new Vector3(-0.15f, 0.1f, 0);
                    break;

                case "right":
                    _transform.position = player.GetComponent<Transform>().position + new Vector3(0.15f, 0.1f, 0);
                    break;

                case "up":
                    _transform.position = player.GetComponent<Transform>().position + new Vector3(0, 0.3f, 0);
                    break;

                case "down":
                    _transform.position = player.GetComponent<Transform>().position;
                    break;

                default:
                    Debug.Log("Player Dir Error");
                    break;
            }
        }
    }
}
