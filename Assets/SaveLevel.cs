using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;

public class SaveLevel : MonoBehaviour
{
    private string dbName = "URI=file:LevelDB.db";

    void Start()
    {
        CreateTable();
        LoadLevel();
    }

    void Update()
    {
        
    }

    private void CreateTable() {
        int rows = 0;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS " +
                    "CustomLevel (BlockID INTEGER DEFAULT 1);";
                command.ExecuteNonQuery();

                command.CommandText = "SELECT COUNT(*) FROM CustomLevel;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        
                        if (!(reader[0] is DBNull)) {
                            rows = Convert.ToInt32(reader[0]);
                        }      

                    reader.Close();
                }

                while (rows < 104) {
                    command.CommandText = "INSERT INTO CustomLevel DEFAULT VALUES;";
                    command.ExecuteNonQuery();
                    rows++;
                }
            }
            connection.Close();
        }

    }

    private void LoadLevel() {
        int currPosition = 0;
        int currBlock = 1;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT BlockID FROM CustomLevel;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) {
                        
                        if (!(reader[0] is DBNull)) {
                            currBlock = Convert.ToInt32(reader[0]);
                        }
                        
                        var x = currPosition % 13 - 6.5f;
                        var y = currPosition / 13 - 3.5f;

                        if (currBlock % 2 == 0) {
                            Instantiate(GameObject.Find("Wall Block"), new Vector3(x, y, 0), Quaternion.identity, GameObject.Find("Placed Blocks").transform);
                        }
                        if (currBlock % 3 == 0) {
                            GameObject.Find("Player").transform.position = new Vector3(x, y, 0);
                            GameObject.Find("Player").transform.SetParent(GameObject.Find("Placed Blocks").transform);
                        }
                        if (currBlock % 5 == 0) {
                            Instantiate(GameObject.Find("Square Block"), new Vector3(x, y, 0), Quaternion.identity, GameObject.Find("Placed Blocks").transform);
                        }
                        if (currBlock % 7 == 0) {
                            Instantiate(GameObject.Find("Circle Block"), new Vector3(x, y, 0), Quaternion.identity, GameObject.Find("Placed Blocks").transform);
                        }
                        if (currBlock % 11 == 0) {
                            Instantiate(GameObject.Find("Bricks Block"), new Vector3(x, y, 0), Quaternion.identity, GameObject.Find("Placed Blocks").transform);
                        }

                        currPosition++;      
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }

    }

    public void SaveLeveltoDB() {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE CustomLevel SET BlockID = 1;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        foreach (Transform item in GameObject.Find("Placed Blocks").transform)
        {
            var rowNumber = item.position.x + 6.5f + (item.position.y + 3.5f) * 13 + 1;
            var tempblockID = 1;
            var blockString = item.gameObject.ToString();

            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT BlockID FROM CustomLevel WHERE rowid = " + rowNumber + ";";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read()) {
                            
                            if (!(reader[0] is DBNull)) {
                                tempblockID = Convert.ToInt32(reader[0]);
                            }
                            if (blockString == "Wall Block(Clone) (UnityEngine.GameObject)") {
                                tempblockID *= 2;
                            }
                            if (blockString == "Player (UnityEngine.GameObject)") {
                                tempblockID *= 3;
                            }
                            if (blockString == "Square Block(Clone) (UnityEngine.GameObject)") {
                                tempblockID *= 5;
                            }
                            if (blockString == "Circle Block(Clone) (UnityEngine.GameObject)") {
                                tempblockID *= 7;
                            }
                            if (blockString == "Bricks Block(Clone) (UnityEngine.GameObject)") {
                                tempblockID *= 11;
                            }
                        }
                        reader.Close();
                    }

                    command.CommandText = "UPDATE CustomLevel SET BlockID = " + tempblockID + " WHERE rowid = " + rowNumber + ";";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
