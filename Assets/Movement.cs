using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 0.7f;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector3 v;

    [SerializeField] private Material myMaterial;

    public bool IsStationary() {
        return Mathf.Abs(_rigidbody.velocity.x) < 0.001f && Mathf.Abs(_rigidbody.velocity.y) < 0.001f;
    }

    private void Bounce() {
        _transform.position = _transform.position + new Vector3(v.x * -0.01f, v.y * -0.01f, 0);
    }

    private void SnapToGrid() {
        Vector3 pos = _transform.position;
        _transform.position = new Vector3(Mathf.Round(pos.x + 0.5f) - 0.5f, Mathf.Round(pos.y + 0.5f) - 0.5f, 0);
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

        var horimovement = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
        if (Input.GetButtonDown("Horizontal") && IsStationary()) {
            _rigidbody.velocity = new Vector2(horimovement * MovementSpeed, 0);
            v = _rigidbody.velocity;
        }

        var vertmovement = Input.GetAxis("Vertical") > 0 ? 1 : -1;
        if (Input.GetButtonDown("Vertical") && IsStationary()) {
            _rigidbody.velocity = new Vector2(0, vertmovement * MovementSpeed);
            v = _rigidbody.velocity;
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
        Bounce();
    }

}
