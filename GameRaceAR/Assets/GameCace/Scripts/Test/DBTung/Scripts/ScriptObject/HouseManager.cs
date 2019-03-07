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
    HouseSpawn houseSpawn;
    // Use this for initialization
    void Start () {
        connectionString = "URI=file:" + Application.dataPath + "/GameCace/Scripts/Test/DBTung/ArDB.db";
        Debug.Log(Application.dataPath);
        Debug.Log(connectionString);
        houseSpawn = GetComponent<HouseSpawn>();
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
                        house.Add(new House(reader.GetString(0), reader.GetString(1)));
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
                        pointhouse.Add(new PointHouse(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6)));  
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    public void InsertHouse(string idMapH, string idHouseH)
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

    public void InsertPointHouse(List<PointHouse> listPointHouses)
    {
        
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                for(int i = 0; i < listPointHouses.Count; i++)
                {
                    string idhouseP = listPointHouses[i].idHouseP;
                    string Hposx = listPointHouses[i].HposxP;
                    string Hposy = listPointHouses[i].HposyP;
                    string Hposz = listPointHouses[i].HposzP;
                    string Hdirx = listPointHouses[i].HdirxP;
                    string Hdiry = listPointHouses[i].HdiryP;
                    string Hdirz = listPointHouses[i].HdirzP;
                    string sqlQuery = String.Format("Insert into TablePointHouse(idhouse,Hposx,Hposy,Hposz,Hdirx,Hdiry,Hdirz) Values(\"{0}\",{1}, {2},{3},{4},{5},{6})", idhouseP, Hposx, Hposy, Hposz, Hdirx, Hdiry, Hdirz);
                    dbCommand.CommandText = sqlQuery;
                    dbCommand.ExecuteScalar();

                }
                
                dbConnection.Close();
            }
        }
    }
}
