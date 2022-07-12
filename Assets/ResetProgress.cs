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
    private Animator transition;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateTable();
        transition = GameObject.Find("CubeZoom").GetComponent<Animator>();

        GameObject.Find("Prev Page").GetComponent<PageButtons>().SavePos();
        GameObject.Find("Next Page").GetComponent<PageButtons>().SavePos();
        GameObject.Find("All Pages").GetComponent<PageMovement>().SetPage();
        GameObject.Find("All Pages").GetComponent<PageMovement>().HideShow();
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS " +
                    "CurrentLevelPage (CurrentPage INTEGER DEFAULT 1);" +
                    " INSERT INTO CurrentLevelPage DEFAULT VALUES;";
                command.ExecuteNonQuery();

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

        IEnumerator GoSelect()
        {
            transition.SetTrigger("EndScene");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Level Select");
        }
        
        StartCoroutine(GoSelect());
    }
}
