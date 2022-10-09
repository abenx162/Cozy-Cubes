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
                                                    "Three's a Crowd", // 5
                                                    "Stepping Stone",
                                                    "Bottleneck",
                                                    "Magic Square",
                                                    "Trial and Error",
                                                    "Gridlocked", // 10
                                                    "Ascending Steps",
                                                    "Broken Staircase",
                                                    "Blossom",
                                                    "Finishing Touches",
                                                    "Minefield", // 15
                                                    "Deja Vu",
                                                    "Lost and Found",
                                                    "Around the World",
                                                    "Middle Trouble",
                                                    "Megamined", // 20
                                                    "Bounce",
                                                    "Trap",
                                                    "Flexible Manoeuvres",
                                                    "Have a Break",
                                                    "Have a Kit Kat", // 25
                                                    "Compact",
                                                    "Glass Wall",
                                                    "In Case of Emergency",
                                                    "Big Fan",
                                                    "Thinking with Portals!"};


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
