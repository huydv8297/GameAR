using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections.Generic;

public class DatabaseConnect : MonoBehaviour
{
    IDbConnection dbcon;
    void Start()
    {
        // Create database
        string connection = "URI=file:" + Application.dataPath + "/Data/" + "database.s3db";
        Debug.Log(connection);
        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Create table
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY, val INTEGER )";

        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();

        // Insert values in table
               
    }

    public void Close()
    {
        // Close connection
        dbcon.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Insert(string command)
    {
         IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO my_table (id, val) VALUES (0, 5)";
        cmnd.ExecuteNonQuery();
    }
    public IDataReader ExcuteCommand(string command)
    {
        
        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;

        cmnd_read.CommandText = command;
        reader = cmnd_read.ExecuteReader();
        return reader;
    }

    public List<int> GetListMap()
    {
        List<int> list = new List<int>();

        string command = "SELECT * FROM map";
        IDataReader reader = ExcuteCommand(command);

        while(reader.Read())
        {
            list.Add(reader.GetInt32(0));
        }
        return list;
    }

    public int GetRoadCount(int idMap)
    {
        string command = "SELECT COUNT(*) FROM road WHERE id_map=" + idMap + " GROUP BY id_road";
        IDataReader reader = ExcuteCommand(command);
        return (int) reader[0];
    }


}