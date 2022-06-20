using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 0.7f;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector3 v;
    private String dir = "left";

    [SerializeField] private Material myMaterial;

    public bool IsStationary() {
        return Mathf.Abs(_rigidbody.velocity.x) < 0.001f && Mathf.Abs(_rigidbody.velocity.y) < 0.001f;
    }

    public String getPlayerDir() {
        return dir;
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

        if (Input.GetButtonDown("Horizontal") && IsStationary()) {
            var horimovement = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            _rigidbody.velocity = new Vector2(horimovement * MovementSpeed, 0);
            v = _rigidbody.velocity;
            dir = horimovement == 1 ? "right" : "left";
        }

        if (Input.GetButtonDown("Vertical") && IsStationary()) {
            var vertmovement = Input.GetAxis("Vertical") > 0 ? 1 : -1;
            _rigidbody.velocity = new Vector2(0, vertmovement * MovementSpeed);
            v = _rigidbody.velocity;
            dir = vertmovement == 1 ? "up" : "down";
        }

        if (gameObject.CompareTag("Untagged")) {
            _rigidbody.velocity = new Vector2(0, 0);
            GetComponent<AudioSource>().Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject != GameObject.Find("Tilemap")) {
            _rigidbody.velocity = v;
        }
        GetComponent<AudioSource>().Play();
        
    }

}
