 using System;
using System.Collections.Generic;
using UnityEngine;

public class PushedAround : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector3 v;
    private String dir = "left";
    
    public bool IsStationary() {
        return Mathf.Abs(_rigidbody.velocity.x) < 0.001f && Mathf.Abs(_rigidbody.velocity.y) < 0.001f;
    }

    private void Bounce() {
        _transform.position = _transform.position + new Vector3(v.x * -0.01f, v.y * -0.01f, 0);
    }

    private void SnapToGrid() {
        Vector3 pos = _transform.position;
        if (dir == "left") {
            _transform.position = new Vector3(Mathf.Ceil(pos.x + 0.5f) - 0.5f, Mathf.Round(pos.y + 0.5f) - 0.5f, 0);
        }
        if (dir == "right") {
            _transform.position = new Vector3(Mathf.Floor(pos.x + 0.5f) - 0.5f, Mathf.Round(pos.y + 0.5f) - 0.5f, 0);
        }
        if (dir == "up") {
            _transform.position = new Vector3(Mathf.Round(pos.x + 0.5f) - 0.5f, Mathf.Floor(pos.y + 0.5f) - 0.5f, 0);
        }
        if (dir == "down") {
            _transform.position = new Vector3(Mathf.Round(pos.x + 0.5f) - 0.5f, Mathf.Ceil(pos.y + 0.5f) - 0.5f, 0);
        }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (gameObject.CompareTag("PlayerGroup")) {
            _rigidbody.velocity = GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity;
        } else {
            _rigidbody.velocity = new Vector2(0, 0);
        }

        v = _rigidbody.velocity;

        dir = GameObject.Find("Player").GetComponent<Movement>().GetPlayerDir();

        if (IsStationary()) {
            SnapToGrid();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("PlayerGroup")) {
            gameObject.tag = "PlayerGroup";
        }

        if (collision.gameObject.CompareTag("Obstacle")) {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("PlayerGroup");   
            foreach (GameObject item in taggedObjects) {
                item.tag = "Untagged";
            }
        }

        if (collision.gameObject.CompareTag("Bounce")) {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("PlayerGroup");   
            foreach (GameObject item in taggedObjects) {
                item.GetComponent<Rigidbody2D>().velocity *= -1;                
            }
            GameObject.Find("Player").GetComponent<Movement>().ReversePlayerDir();
        }

        if (collision.gameObject.CompareTag("OrangePortal")) {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("OrangePortal");   
            foreach (GameObject item in taggedObjects) {
                if (item != collision.gameObject) {
                    _transform.position = item.GetComponent<Transform>().position + (Vector3) item.GetComponent<Portal>().direction;
                }
            }
        }

        Bounce();
    }
}
