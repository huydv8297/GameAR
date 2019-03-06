using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
using Mono.Data.Sqlite;

public class HouseManager : MonoBehaviour {
    private string connectionString;
    private List<House> house = new List<House>();
    private List<PointHouse> pointhouse = new List<PointHouse>();
    // Use this for initialization
    void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/GameCace/Scripts/Test/DBTung/ArDB.db";
        Debug.Log(Application.dataPath);
        Debug.Log(connectionString);
        //InsertHouse(1, 1);
        GetHouse();
        GetPointHouse();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void GetHouse()
    {
        house.Clear();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = "SELECT * FROM TableHouse";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Debug.Log("id: " + reader.GetInt32(0) + "id2: " + reader.GetInt32(1));
                        house.Add(new House(reader.GetInt32(0), reader.GetInt32(1)));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void GetPointHouse()
    {
        pointhouse.Clear();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = "SELECT * FROM TablePointHouse";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        pointhouse.Add(new PointHouse(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)));  
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void InsertHouse(int idMapH, int idHouseH)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("Insert into TableHouse(idMapH,idHouse) Values(\"{0}\",{1})", idMapH, idHouseH);
                dbCommand.CommandText = sqlQuery;
                dbCommand.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void InsertPointHouse(int idhouseP, int idpointhouseP, string Hposx, string Hposy, string Hposz, string Hdirx, string Hdiry, string Hdirz)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("Insert into TablePointHouse(idhouse,idPointHouse,Hposx,Hposy,Hposz,Hdirx,Hdiry,Hdirz) Values(\"{0}\",{1}, {2},{3},{4},{5},{6},{7})", idhouseP, idpointhouseP, Hposx, Hposy, Hposz, Hdirx, Hdiry, Hdirz);
                dbCommand.CommandText = sqlQuery;
                dbCommand.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }
}
