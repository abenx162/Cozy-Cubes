using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using Mono.Data.Sqlite;

public class ResetProgress : MonoBehaviour
{

    private string dbName = "URI=file:LevelDB.db";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE LevelTable SET Completed = 0, Unlocked = 0, BestScore = 0" + ";";
                command.ExecuteNonQuery();
                command.CommandText = "UPDATE LevelTable SET Unlocked = 1 WHERE ID = " + 1 + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
