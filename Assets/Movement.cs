
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 0.7f;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector3 v;


    private bool IsStationary() {
        return Mathf.Abs(_rigidbody.velocity.x) < 0.001f && Mathf.Abs(_rigidbody.velocity.y) < 0.001f;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        gameObject.tag = "PlayerGroup";
    }

    private void Update()
    {
        
        if (IsStationary()) {
            Vector3 pos = _transform.position;
            _transform.position = new Vector3(Mathf.Round(pos.x + 0.5f) - 0.5f, Mathf.Round(pos.y + 0.5f) - 0.5f, 0);
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
        }

        //Debug.Log(v);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        _rigidbody.velocity = v;

        if (collision.gameObject == GameObject.Find("Tilemap")) {
            _rigidbody.velocity = new Vector2(0, 0);
        }

        _transform.position = _transform.position + new Vector3(v.x * -0.01f, v.y * -0.01f, 0);
    }

}
