using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubesUndo : MonoBehaviour
{
    private Stack<Vector3> PrevPos = new Stack<Vector3>();
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
        PrevPos = new Stack<Vector3>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
   
    void Update()
    {
        bool stopped = GameObject.Find("Player").GetComponent<Movement>().IsStationary();

        if (GameObject.Find("Player").GetComponent<Movement>().controllable)
        { 
            if (Input.GetButtonDown("Horizontal") && stopped)
            {
                PrevPos.Push(new Vector3(Mathf.Round(_transform.position.x + 0.5f) - 0.5f, Mathf.Round(_transform.position.y + 0.5f) - 0.5f, 0));
            }

            if (Input.GetButtonDown("Vertical") && stopped)
            {
                PrevPos.Push(new Vector3(Mathf.Round(_transform.position.x + 0.5f) - 0.5f, Mathf.Round(_transform.position.y + 0.5f) - 0.5f, 0));
            }
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
