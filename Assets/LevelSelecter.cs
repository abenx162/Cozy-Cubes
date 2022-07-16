using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private Animator transition;

    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        txt = btn.GetComponentInChildren<TextMeshProUGUI>().text;
        colorblk = btn.colors;
        transition = GameObject.Find("CubeZoom").GetComponent<Animator>();

        if (!IsUnlocked(txt)) {
            btn.interactable = false;
        }

        if (IsCompleted(txt)) {
            colorblk.normalColor = new Color32(25, 195, 44, 255);
            colorblk.highlightedColor = new Color32(15, 185, 34, 255);
            colorblk.pressedColor = new Color32(0, 170, 19, 255);
            btn.colors = colorblk;
        }

        
        if(IsTarget(txt))
        {
            GameObject medalTemplate = GameObject.Find("Medal Template");
            Vector3 pos = btn.transform.position + new Vector3(80, 5, 0);
            GameObject medalClone = GameObject.Instantiate(medalTemplate, pos, Quaternion.identity, btn.transform);
        }
    }

    void Update()
    {
        
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

    private bool IsTarget(string id)
    {
        bool result = false;

        if (IsCompleted(id))
        {
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM LevelTable WHERE ID = " + id + ";";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return (Convert.ToInt32(reader["BestScore"]) <= Convert.ToInt32(reader["TargetScore"]));
                        }

                        reader.Close();
                    }
                }
                connection.Close();
            }
        }
        
        return result;
    }

    public void GoToLevel()
    {
        IEnumerator GoLevel()
        {
            transition.SetTrigger("EndScene");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Level " + txt);
        }

        if (IsUnlocked(txt))
        {
            StartCoroutine(GoLevel());
        }
    }

    public void Hovered()
    {
        GameObject.Find("Level Title").GetComponent<LevelName>().SetLevelText(Int32.Parse(txt));
    }
}
