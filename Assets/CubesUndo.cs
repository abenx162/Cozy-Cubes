using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubesUndo : MonoBehaviour
{
    private Stack<Vector3> PrevPos;
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    public int Moves()
    {
        return PrevPos.Count;
    }

    public void Undo() {
        if (PrevPos.Count != 0) {
            _transform.position = PrevPos.Pop();
        }
    }

    void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
   
    void Update()
    {
        bool stopped = GameObject.Find("Player").GetComponent<Movement>().IsStationary();

        if (Input.GetButtonDown("Horizontal") && stopped) {
            PrevPos.Push(_transform.position);
        }

        if (Input.GetButtonDown("Vertical") && stopped) {
            PrevPos.Push(_transform.position);
        }
    }

    void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }
         
    void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
         
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            PrevPos = new Stack<Vector3>();
        }
}
