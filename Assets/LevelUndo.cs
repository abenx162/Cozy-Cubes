using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUndo : MonoBehaviour
{
    public void UndoLevel() {
        if (!GameObject.Find("Player").GetComponent<Movement>().IsStationary()) {
            return;
        }
        Object[] blocks = Object.FindObjectsOfType<CubesUndo>();
        foreach (Object item in blocks)
        {
            CubesUndo script = (CubesUndo) item;
            script.Undo();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("u")) {
            UndoLevel();
        }
    }
}
