using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    [SerializeField] private Material myMaterial;
    private int inGoal;

    public bool Done() {
        return inGoal == 1 && gameObject.GetComponent<PushedAround>().IsStationary();
    }

    void Start() {
        inGoal = 0;
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = new Color32(192, 28, 28, 255);
    }

    void Update() {
        if (inGoal == 0) {
            myMaterial.color = new Color32(192, 28, 28, 255);
        } else {
            myMaterial.color = new Color32(25, 195, 44, 255);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Goal")) {
            inGoal++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Goal")) {
            inGoal--;
        }
    }
}
