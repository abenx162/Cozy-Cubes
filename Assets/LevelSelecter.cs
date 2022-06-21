using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class LevelSelecter : MonoBehaviour
{
    private string dbName = "URI=file:LevelDB.db";

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("r")) {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    private bool IsUnlocked(string id)
    {
        bool result = false;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM LevelTable WHERE ID = " + id + ";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())

                        // Debug.Log(reader["Unlocked"]);
                        return Convert.ToBoolean(reader["Unlocked"]);
                    
                    reader.Close();
                }
            }
            connection.Close();
        }
        return result;
    }

    public void GoToLevel1()
    {
        if (IsUnlocked("1"))
        {
            SceneManager.LoadScene("Level 1");
        }        
    }

    public void GoToLevel2()
    {
        if (IsUnlocked("2"))
        {
            SceneManager.LoadScene("Level 2");
        }
    }

    public void GoToLevel3()
    {
        if (IsUnlocked("3"))
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public void GoToLevel4()
    {
        if (IsUnlocked("4"))
        {
            SceneManager.LoadScene("Level 4");
        }
    }

    public void GoToLevel5()
    {
        SceneManager.LoadScene("Level 5");
    }

    public void GoToLevel6()
    {
        SceneManager.LoadScene("Level 6");
    }

    public void GoToLevel7()
    {
        SceneManager.LoadScene("Level 7");
    }

    public void GoToLevel8()
    {
        SceneManager.LoadScene("Level 8");
    }

    public void GoToLevel9()
    {
        SceneManager.LoadScene("Level 9");
    }

    public void GoToLevel10()
    {
        SceneManager.LoadScene("Level 10");
    }

    

}
