using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Data;
using Mono.Data.Sqlite;

public class MovesCounter : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private string dbName = "URI=file:LevelDB.db";
    private int oldcount = 0;
    public int levelTarget = 0;
    private int levelID;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        levelID = SceneManager.GetActiveScene().buildIndex;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM LevelTable WHERE ID = " + levelID + ";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oldcount = Convert.ToInt32(reader["BestScore"]);
                        levelTarget = Convert.ToInt32(reader["TargetScore"]);
                    }                           
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int count = GameObject.Find("Player").GetComponent<CubesUndo>().Moves();
        textMeshPro.SetText("Moves: " + count + "\n" + "Previous Best: " + (oldcount == 0 ? "-" : oldcount) + "\n" + "Target: " + levelTarget);
    }
}
