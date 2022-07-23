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
    private Animator transition;
    private bool DBupdated = false;

    void Start()
    {
        levelID = SceneManager.GetActiveScene().buildIndex;
        transition = GameObject.Find("CubeZoom").GetComponent<Animator>();
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Movement>().IsStationary() && AllClear() && !DBupdated) {
            WinTxt.SetActive(true);
            DBupdated = true;

            // records level as completed, unlocks next level, updates high score
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    int levelDoneBefore = 0;
                    command.CommandText = "SELECT Completed from LevelTable WHERE ID = " + levelID + ";";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                             levelDoneBefore = Convert.ToInt32(reader[0]);
                        reader.Close();
                    }

                    if (levelDoneBefore == 0) {
                        int nextunlock = 0;
                        command.CommandText = "SELECT ID from LevelTable WHERE Unlocked = 0 LIMIT 1;";
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                nextunlock = Convert.ToInt32(reader[0]);
                            reader.Close();
                        }

                        command.CommandText = "UPDATE LevelTable SET Unlocked = 1 WHERE ID = " 
                            + nextunlock + ";";
                        command.ExecuteNonQuery();
                    }

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

            GameObject.Find("Player").GetComponent<Movement>().controllable = false;
            StartCoroutine(GoSelect());
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

    IEnumerator GoSelect()
    {
        yield return new WaitForSeconds(1);
        transition.SetTrigger("EndScene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level Select");
    }

        
}
