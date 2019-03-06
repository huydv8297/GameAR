using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
using Mono.Data.Sqlite;
public class RoadManagerDB : MonoBehaviour {
    private string connectionString;
    private List<Road> road = new List<Road>();
    private List<PointRoad> pointroad = new List<PointRoad>();
    // Use this for initialization
    void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/GameCace/Scripts/Test/DBTung/ArDB.db";
        Debug.Log(Application.dataPath);
        Debug.Log(connectionString);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void GetRoad()
    {
        road.Clear();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = "SELECT * FROM TableRoad";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        road.Add(new Road(reader.GetInt32(0), reader.GetInt32(1)));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void GetPointRoad()
    {
        pointroad.Clear();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = "SELECT * FROM TablePointRoad";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        pointroad.Add(new PointRoad(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }
    private void InsertRoad(int idMapR, int idRoadR)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("Insert into TableRoad(idMapR,idRoad) Values(\"{0}\",{1})", idMapR, idRoadR);
                dbCommand.CommandText = sqlQuery;
                dbCommand.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void InsertPointRoad(int idRoadP, int idpointRoadP, string Rposx, string Rposy, string Rposz, string Rdirx, string Rdiry, string Rdirz)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("Insert into TablePointRoad(idRoad,idPointRoad,Rposx,Rposy,Rposz,Rdirx,Rdiry,Rdirz) Values(\"{0}\",{1}, {2},{3},{4},{5},{6},{7})", idRoadP, idpointRoadP, Rposx, Rposy, Rposz, Rdirx, Rdiry, Rdirz);
                dbCommand.CommandText = sqlQuery;
                dbCommand.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }


}
