using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMap : MonoBehaviour {
    HouseSpawn houseSpawn;
    HouseManager manager;
    // Use this for initialization
    void Start () {
        houseSpawn = GetComponent<HouseSpawn>();
        manager = GetComponent<HouseManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BtnSave()
    {
        List <GameObject> gameObjects = houseSpawn.listHouse;
        List<PointHouse> pointHouses = new List<PointHouse>();
        for(int i=0;i<gameObjects.Count; i++)
        {
            string iPointHouse = "anh_" + i;
            string Hposx = gameObjects[i].transform.position.x.ToString();
            string Hposy = gameObjects[i].transform.position.y.ToString();
            string Hposz = gameObjects[i].transform.position.z.ToString();
            string Hdirx = gameObjects[i].transform.rotation.x.ToString();
            string Hdiry = gameObjects[i].transform.rotation.y.ToString();
            string Hdirz = gameObjects[i].transform.rotation.z.ToString();
            PointHouse pointHouse = new PointHouse(iPointHouse, Hposx, Hposy, Hposz, Hdirx, Hdiry, Hdirz);
            pointHouses.Add(pointHouse);
        }
        if(pointHouses.Count == 0)
        {
            return;
        }
        manager.InsertPointHouse(pointHouses);
    }
}
