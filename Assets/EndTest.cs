using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject.Find("Test Play").transform.position = gameObject.transform.position;
        gameObject.transform.position += new Vector3(1000, 0, 0);
        
        StartCoroutine(DisableScripts());

        IEnumerator DisableScripts() {
        yield return new WaitForSeconds(0.1f);
        
        foreach (Transform item in GameObject.Find("Placed Blocks").transform) {
            MonoBehaviour[] scripts = item.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts) {
                script.enabled = false;
                }
            }
        }
        
    }
}
