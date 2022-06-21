using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class ResetProgress : MonoBehaviour
{

    private string dbName = "URI=file:LevelDB.db";
    
    // Start is called before the first frame update
    void Start()
    {
        CreateTable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTable() {
        int rows = 0;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS LevelTable (" +
	                                    "ID	INTEGER," +
                                        "Completed INTEGER DEFAULT 0," +
                                        "Unlocked INTEGER DEFAULT 0," +
                                        "BestScore INTEGER DEFAULT 0," +
                                        "PRIMARY KEY(ID AUTOINCREMENT)" +
                                        ");";
                command.ExecuteNonQuery();

                command.CommandText = "SELECT COUNT(*) FROM LevelTable;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        
                        if (!(reader[0] is DBNull)) {
                            rows = Convert.ToInt32(reader[0]);
                        }      

                    reader.Close();
                }

                while (rows < 100) {
                    command.CommandText = "INSERT INTO LevelTable DEFAULT VALUES;";
                    command.ExecuteNonQuery();
                    rows++;
                }

                command.CommandText = "UPDATE LevelTable SET Unlocked = 1 WHERE ID = 1;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

    }

    public void ResetDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE LevelTable SET Completed = 0, Unlocked = 0, BestScore = 0;";
                command.ExecuteNonQuery();
                command.CommandText = "UPDATE LevelTable SET Unlocked = 1 WHERE ID = 1;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        SceneManager.LoadScene("Level Select");
    }
}
