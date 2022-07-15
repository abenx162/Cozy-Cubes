using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelName : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private string[] levelNamelist = new string[] {"",
                                                    "Cozy Corner",
                                                    "Two's a Party",
                                                    "Three's a Crowd",
                                                    "Magic Square",
                                                    "Trial and Error",
                                                    "Gridlocked",
                                                    "Dilemma",
                                                    "So Close",
                                                    "Yet So Far",
                                                    "Minefield",
                                                    "Bounce Pad Demo",
                                                    "Fragile Blocks Demo",
                                                    "Portal Demo"};


    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        textMeshPro.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelText(int n) 
    {
        textMeshPro.SetText(n + ". " + levelNamelist[n]);
    }
}
