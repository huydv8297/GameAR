using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class MapManager : MonoBehaviour {

	public DatabaseConnect database;
    public RoadManager roadManager;
    public HouseSpawn houseSpawn;

    public int currentMap;
    public List<Spline> listRoad;
    public List<GameObject> listHouse;

    public List<RoadData> mapData;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void LoadData()
    {
        mapData = new List<RoadData>();
        int count = database.GetRoadCount(currentMap);
        for (int i = 0; i < count; i++)
        {
            string cmd = "SELECT * FROM road WHERE id_map=" + currentMap + " AND id_road=" + i;
            IDataReader reader = database.ExcuteCommand(cmd);
            RoadData data = new RoadData();
            data.position = new List<Vector3>();
            data.direction = new List<Vector3>();
            while (reader.Read())
            {
                
                Vector3 position = new Vector3(reader.GetFloat(3), reader.GetFloat(4), reader.GetFloat(5));
                Vector3 direction = new Vector3(reader.GetFloat(6), reader.GetFloat(7), reader.GetFloat(8));
                data.position.Add(position);
                data.direction.Add(direction);
            }
            mapData.Add(data);
        }

       // roadManager.LoadData(mapData);
    }

    public void SaveData()
    {
        listRoad = roadManager.splines;
        listHouse = houseSpawn.listHouse;
        SaveMap();
        SaveRoad();
    }


    public void SaveMap()
    {
        currentMap = GetRanDomMapID();
        string cmd = "INSERT INTO map(id_map) VALUES ( " + currentMap + ")";
        database.Insert(cmd);
    }

    public void SaveRoad()
    {
        string cmd = "INSERT INTO road VALUES ";
        for(int i = 0; i < listRoad.Count; i++)
        {

            for(int j = 0; j < listRoad[i].nodes.Count; j++)
            {
                if (i != 0 && j != 0)
                    cmd += ",";
                cmd += $"( {currentMap}, {i}, {j}, {listRoad[i].nodes[j].position.x}, {listRoad[i].nodes[j].position.y}, {listRoad[i].nodes[j].position.z}, {listRoad[i].nodes[j].direction.x}, {listRoad[i].nodes[j].direction.x}, {listRoad[i].nodes[j].direction.x})";
            }
        }
        database.Insert(cmd);
    }


    public int GetRanDomMapID()
    {
        int id = Random.Range(1000, 9999);
        List<int> listId = database.GetListMap();

        while(listId.Contains(id))
        {
            id = Random.Range(1000, 9999);
        }
        return id;
    }

    
}

public struct RoadData
{
    public List<Vector3> position;
    public List<Vector3> direction;
}
