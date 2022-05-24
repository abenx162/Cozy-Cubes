using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedAround : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector3 v;
    
    private bool IsStationary() {
        return Mathf.Abs(_rigidbody.velocity.x) < 0.001f && Mathf.Abs(_rigidbody.velocity.y) < 0.001f;
    }

    private void Bounce() {
        _transform.position = _transform.position + new Vector3(v.x * -0.01f, v.y * -0.01f, 0);
    }

    private void SnapToGrid() {
        Vector3 pos = _transform.position;
        _transform.position = new Vector3(Mathf.Round(pos.x + 0.5f) - 0.5f, Mathf.Round(pos.y + 0.5f) - 0.5f, 0);
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

        if (IsStationary()) {
            SnapToGrid();
        }  
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("PlayerGroup")) {
            gameObject.tag = "PlayerGroup";
        }

        if (collision.gameObject == GameObject.Find("Tilemap")) {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("PlayerGroup");   
            foreach (GameObject item in taggedObjects) {
                item.tag = "Untagged";
            }
        }

        Bounce();
    }
}
