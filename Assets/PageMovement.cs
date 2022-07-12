using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class PageMovement : MonoBehaviour
{
    private string dbName = "URI=file:LevelDB.db";
    private int maxPage;

    // Start is called before the first frame update
    void Start()
    {
        maxPage = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MovePage(Vector3 start, Vector3 end, float seconds)
    {
        GameObject.Find("Prev Page").GetComponent<Button>().interactable = false;
        GameObject.Find("Next Page").GetComponent<Button>().interactable = false;

        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(start, end, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        GameObject.Find("Prev Page").GetComponent<Button>().interactable = true;
        GameObject.Find("Next Page").GetComponent<Button>().interactable = true;
    }

    public int GetPageNum()
    {
        int result = 1;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM CurrentLevelPage WHERE ROWID = 1";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())

                        result = Convert.ToInt32(reader["CurrentPage"]);

                    reader.Close();
                }
            }
            connection.Close();
        }

        return result;
    }

    public void SetPageNum(int num)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE CurrentLevelPage SET CurrentPage = " + num + " WHERE ROWID = 1;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void SetPage()
    {
        transform.position += (GetPageNum() - 1) * new Vector3(-Screen.width, 0, 0);
    }

    public void HideShow()
    {
        if (GetPageNum() == maxPage)
        {
            GameObject.Find("Next Page").GetComponent<PageButtons>().Hide();
        } else
        {
            GameObject.Find("Next Page").GetComponent<PageButtons>().Show();
        }

        if (GetPageNum() == 1)
        {
            GameObject.Find("Prev Page").GetComponent<PageButtons>().Hide();
        } else
        {
            GameObject.Find("Prev Page").GetComponent<PageButtons>().Show();
        }
    }

    public void NextPage()
    {
        int currentPage = GetPageNum();
        if (currentPage < maxPage)
        {
            Vector3 start = transform.position;
            Vector3 end = start + new Vector3(-Screen.width, 0, 0);
            StartCoroutine(MovePage(start, end, 1));

            SetPageNum(currentPage + 1);

            HideShow();
        }
    }

    public void PrevPage()
    {
        int currentPage = GetPageNum();
        if (currentPage > 1)
        {
            Vector3 start = transform.position;
            Vector3 end = start + new Vector3(Screen.width, 0, 0);
            StartCoroutine(MovePage(start, end, 1));
            
            SetPageNum(currentPage - 1);

            HideShow();
        }
    }
}
