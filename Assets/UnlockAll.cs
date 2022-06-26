using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class UnlockAll : MonoBehaviour
{
    private string dbName = "URI=file:LevelDB.db";

    public void UnlockAllDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE LevelTable SET Completed = 1, Unlocked = 1, BestScore = 0;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        SceneManager.LoadScene("Level Select");
    }
}
