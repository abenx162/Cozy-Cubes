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
                                                    "Lucky Clover",
                                                    "Initial Prep",
                                                    "Three's a Crowd",
                                                    "Stepping Stone",
                                                    "Bottleneck",
                                                    "Magic Square",
                                                    "Trial and Error",
                                                    "Gridlocked",
                                                    "Ascending Steps",
                                                    "Broken Staircase",
                                                    "Blossom",
                                                    "Finishing Touches",
                                                    "Minefield",
                                                    "Deja Vu",
                                                    "Lost and Found",
                                                    "Around the World",
                                                    "Middle Trouble",
                                                    "Megamined",
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
