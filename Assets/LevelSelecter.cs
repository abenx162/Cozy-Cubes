using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using TMPro;

public class LevelSelecter : MonoBehaviour
{
    private string dbName = "URI=file:LevelDB.db";
    private Button btn;
    private string txt;
    private ColorBlock colorblk;

    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        txt = btn.GetComponentInChildren<TextMeshProUGUI>().text;
        colorblk = btn.colors;

        if (txt != "Restart" && txt != "Select Level" && !IsUnlocked(txt)) {
            btn.interactable = false;
        }

        if (txt != "Restart" && txt != "Select Level" && IsCompleted(txt)) {
            colorblk.normalColor = new Color32(25, 195, 44, 255);
            colorblk.highlightedColor = new Color32(15, 185, 34, 255);
            colorblk.pressedColor = new Color32(0, 170, 19, 255);
            btn.colors = colorblk;
        }
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

                        return Convert.ToBoolean(reader["Unlocked"]);
                    
                    reader.Close();
                }
            }
            connection.Close();
        }
        return result;
    }

    private bool IsCompleted(string id)
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

                        return Convert.ToBoolean(reader["Completed"]);
                    
                    reader.Close();
                }
            }
            connection.Close();
        }
        return result;
    }

    public void GoToLevel()
    {
        if (IsUnlocked(txt))
        {
            SceneManager.LoadScene("Level " + txt);
        }
    }

    public void GoToBounce()
    {
        SceneManager.LoadScene("Bounce Pad Test");
    }

    public void GoToPortal()
    {
        SceneManager.LoadScene("Portal Test");
    }

    public void GoToBreakable()
    {
        SceneManager.LoadScene("Fragile Test");
    }
}
