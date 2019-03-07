using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
using Mono.Data.Sqlite;

public class CarDB : MonoBehaviour {
    private string connectionString;
	// Use this for initialization
	void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/GameCace/Scripts/Test/DBTung/ArDB.db";
        Debug.Log(Application.dataPath);
        Debug.Log(connectionString);
        GetCar();
        //InsertScore();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void GetCar()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                
                string sqlQuery = "SELECT * FROM TableCars";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    
                   
                    while (reader.Read())
                    {

                        Debug.Log(reader.GetInt32(0));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }
}
