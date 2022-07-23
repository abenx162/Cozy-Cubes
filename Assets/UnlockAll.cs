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
    private Animator transition;

    void Start()
    {
        transition = GameObject.Find("CubeZoom").GetComponent<Animator>();
    }

    public void UnlockAllDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE LevelTable SET Unlocked = 1;";
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
