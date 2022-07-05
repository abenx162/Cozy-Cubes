using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float MovementSpeed = 5;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector3 v;
    private string dir = "left";
    public bool controllable = true;

    [SerializeField] private Material myMaterial;

    public bool IsStationary() {
        return Mathf.Abs(_rigidbody.velocity.x) < 0.001f && Mathf.Abs(_rigidbody.velocity.y) < 0.001f;
    }

    public String GetPlayerDir() {
        return dir;
    }

    public void ReversePlayerDir() {
        _rigidbody.velocity = -v;
        v = _rigidbody.velocity;
        dir = _rigidbody.velocity.x > 0 ? "right"
                  : _rigidbody.velocity.x < 0
                  ? "left"
                  : _rigidbody.velocity.y > 0
                  ? "up"
                  : "down";
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

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = new Color32(0, 178, 255, 255);
    }

    private void Update()
    {
        if (IsStationary()) {
            SnapToGrid();
            gameObject.tag = "PlayerGroup";
        }    

        if (controllable)
        {
            if (Input.GetButtonDown("Horizontal") && IsStationary())
            {
                var horimovement = Input.GetAxisRaw("Horizontal");
                _rigidbody.velocity = new Vector2(horimovement * MovementSpeed, 0);
                v = _rigidbody.velocity;
                dir = horimovement == 1 ? "right" : "left";
            }

            if (Input.GetButtonDown("Vertical") && IsStationary())
            {
                var vertmovement = Input.GetAxisRaw("Vertical");
                _rigidbody.velocity = new Vector2(0, vertmovement * MovementSpeed);
                v = _rigidbody.velocity;
                dir = vertmovement == 1 ? "up" : "down";
            }

            if (gameObject.CompareTag("Untagged"))
            {
                _rigidbody.velocity = new Vector2(0, 0);
                GetComponent<AudioSource>().Play();
            }
        }     
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject != GameObject.Find("Tilemap")) {
            _rigidbody.velocity = v;
        }

        if (collision.gameObject == GameObject.Find("Tilemap")) {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("PlayerGroup");   
            foreach (GameObject item in taggedObjects) {
                item.tag = "Untagged";
            }
        }

        if (collision.gameObject.CompareTag("Bounce")) {
            ReversePlayerDir();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("PlayerGroup");
            foreach (GameObject item in taggedObjects)
            {
                item.tag = "Untagged";
            }
        }

        if (collision.gameObject.CompareTag("OrangePortal")) {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("OrangePortal");   
            foreach (GameObject item in taggedObjects) {
                if (item != collision.gameObject) {
                    _transform.position = item.GetComponent<Transform>().position + (Vector3) item.GetComponent<Portal>().direction;
                    _rigidbody.velocity = v;
                }
            }
        }

        GetComponent<AudioSource>().Play();
        
    }

}
