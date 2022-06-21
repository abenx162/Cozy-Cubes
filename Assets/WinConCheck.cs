using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mono.Data.Sqlite;

public class WinConCheck : MonoBehaviour
{
    public GameObject WinTxt;
    private string dbName = "URI=file:LevelDB.db";
    private int levelID;

    void Start()
    {
        levelID = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Movement>().IsStationary() && AllClear()) {
            WinTxt.SetActive(true);

            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE LevelTable SET Unlocked = 1 WHERE ID = " 
                        + (levelID + 1) + ";";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE LevelTable SET Completed = 1 WHERE ID = "
                        + levelID + ";";
                    command.ExecuteNonQuery();

                    int oldcount = 0;
                    command.CommandText = "SELECT * FROM LevelTable WHERE ID = " + levelID + ";";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                             oldcount = Convert.ToInt32(reader["BestScore"]);
                        reader.Close();
                    }

                    int newcount = GameObject.Find("Player").GetComponent<CubesUndo>().Moves();
                    if (oldcount == 0 || newcount < oldcount)
                    {
                        command.CommandText = "UPDATE LevelTable SET " + "BestScore" + " = " + newcount + " WHERE ID = "
                        + levelID + ";";
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }

            Invoke("ReturnToLevelSelect", 1);
        }
    }

    private bool AllClear() {
        bool clear = true;
        UnityEngine.Object[] blocks = UnityEngine.Object.FindObjectsOfType<ChangeColour>();
        foreach (UnityEngine.Object item in blocks)
        {
            ChangeColour script = (ChangeColour) item;
            clear = clear && script.Done();
        }
        return clear;
    }

    private void ReturnToLevelSelect() {
        SceneManager.LoadScene("Level Select");
    }
}
