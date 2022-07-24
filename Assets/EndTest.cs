using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deactivate()
    {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        while (GameObject.Find("Player").GetComponent<CubesUndo>().Moves() > 0) {
            GameObject.Find("Temp Undo Button").GetComponent<LevelUndo>().UndoLevel();
        }

        GameObject.Find("Temp Undo Button").GetComponent<LevelUndo>().enabled = false;

        GameObject.Find("Test Play").transform.position = gameObject.transform.position;
        gameObject.transform.position += new Vector3(1000, 0, 0);
        GameObject.Find("Wall Button").GetComponent<Button>().interactable = true;
        GameObject.Find("Player Button").GetComponent<Button>().interactable = true;
        GameObject.Find("Square Button").GetComponent<Button>().interactable = true;
        GameObject.Find("Circle Button").GetComponent<Button>().interactable = true;
        GameObject.Find("Bricks Button").GetComponent<Button>().interactable = true;
        GameObject.Find("Eraser Button").GetComponent<Button>().interactable = true;
        GameObject.Find("Save Level").GetComponent<Button>().interactable = true;
        
        StartCoroutine(DisableScripts());

        IEnumerator DisableScripts() {
            yield return new WaitForSeconds(0.1f);
        
            foreach (Transform item in GameObject.Find("Placed Blocks").transform) {
                MonoBehaviour[] scripts = item.gameObject.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts) {
                    script.enabled = false;                                     
                }
                item.GetComponent<Erasable>().enabled = true;
            }
        }
        
    }
}
