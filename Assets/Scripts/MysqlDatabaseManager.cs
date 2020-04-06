using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using TMPro;
using UnityEngine.UI;

public class MysqlDatabaseManager : MonoBehaviour
{
    [Header("Connection Details")]
    [SerializeField]
    private string _ConnectionServerIP;
    [SerializeField]
    private string _ConnectionDatabaseName;
    [SerializeField]
    private string _ConnectionUsername;
    [SerializeField]
    private string _ConnectionPassword;

    [SerializeField]
    private TextMeshProUGUI _UiText;

    [SerializeField]
    private int _PlayerID;
    [SerializeField]
    private int _PlayerScore;
    [SerializeField]
    private string _PlayerName;
    [SerializeField]
    private string _TableName;
    [SerializeField]
    private string _ColumnName1;
    [SerializeField]
    private string _ColumnName2;

    // Start is called before the first frame update
    void Start()
    {
        string _connectionString = null;
        _connectionString = "server=" + _ConnectionServerIP + ";database=" + _ConnectionDatabaseName + ";uid=" + _ConnectionUsername + ";pwd=" + _ConnectionPassword;
        MySqlConnection _conn = new MySqlConnection(_connectionString);
        try
        {
            Debug.Log("Starting Connection");
            _conn.Open();
            Debug.Log("Connection Opened");
            _conn.Close();
        }
        catch (Exception ex)
        {
            Debug.Log("Connection Failed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertScore()
    {
        string _connectionString = null;
        _connectionString = "server=" + _ConnectionServerIP + ";database=" + _ConnectionDatabaseName + ";uid=" + _ConnectionUsername + ";pwd=" + _ConnectionPassword;
        MySqlConnection _conn = new MySqlConnection(_connectionString);
        try
        {
            Debug.Log("Starting Connection");
            _conn.Open();
            Debug.Log("Inserting a score into the database");
            string _Sql = "INSERT INTO " + _TableName + " VALUES('" + _PlayerID + "', '" + _PlayerScore +"', '" + _PlayerName + "')";
            MySqlCommand _cmd = new MySqlCommand(_Sql, _conn);
            _cmd.ExecuteNonQuery();
            Debug.Log("Score added.");
            _conn.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
    public void DeleteScore()
    {
        string _connectionString = null;
        _connectionString = "server=" + _ConnectionServerIP + ";database=" + _ConnectionDatabaseName + ";uid=" + _ConnectionUsername + ";pwd=" + _ConnectionPassword;
        MySqlConnection _conn = new MySqlConnection(_connectionString);
        try
        {
            Debug.Log("Starting Connection");
            _conn.Open();
            Debug.Log("Deleting a score from the database.");
            string _Sql = "DELETE FROM "+ _TableName + " WHERE " + _ColumnName1 + " = " + _PlayerID;
            MySqlCommand _cmd = new MySqlCommand(_Sql, _conn);
            _cmd.ExecuteNonQuery();
            Debug.Log("Score Delete.");
            _conn.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void UpdateScore()
    {
        string _connectionString = null;
        _connectionString = "server=" + _ConnectionServerIP + ";database=" + _ConnectionDatabaseName + ";uid=" + _ConnectionUsername + ";pwd=" + _ConnectionPassword;
        MySqlConnection _conn = new MySqlConnection(_connectionString);
        try
        {
            Debug.Log("Starting Connection");
            _conn.Open();
            Debug.Log("Updating a score from the database.");
            string _Sql = "UPDATE " + _TableName + " SET " + _ColumnName1 + " = '" + _PlayerScore + "' WHERE " + _ColumnName2 +  " = " + _PlayerID + ";";
            MySqlCommand _cmd = new MySqlCommand(_Sql, _conn);
            _cmd.ExecuteNonQuery();
            Debug.Log("Score Updated.");
            _conn.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void ShowScore()
    {
        string _connectionString = null;
        _connectionString = "server=" + _ConnectionServerIP + ";database=" + _ConnectionDatabaseName + ";uid=" + _ConnectionUsername + ";pwd=" + _ConnectionPassword;
        MySqlConnection _conn = new MySqlConnection(_connectionString);
        try
        {
            Debug.Log("Starting Connection");
            _conn.Open();
            Debug.Log("Deleting a score from the database.");
            string _Sql = "SELECT " + _ColumnName1 + " FROM " + _TableName;
            MySqlCommand _cmd = new MySqlCommand(_Sql, _conn);
            MySqlDataReader rdr = _cmd.ExecuteReader();
            int highscore = 0;
            while(rdr.Read())
            {
                for(int i = 0; i < rdr.FieldCount; i++)
                {
                    Debug.Log(rdr[i]);
                    int currentscore = Convert.ToInt32(rdr[i]);
                    if(currentscore > highscore)
                    {
                        highscore = currentscore;
                    }
                }
                _UiText.text = "Highscore: " + highscore;
            }
            Debug.Log("Score Read and Set.");
            _conn.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
}
