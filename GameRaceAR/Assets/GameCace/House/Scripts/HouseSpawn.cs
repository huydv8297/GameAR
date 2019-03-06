using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseSpawn : MonoBehaviour, CursorEvent
{
    public GameObject housePrefab;
    public Toggle isActive;
    public bool isCreatale = true;
    public List<GameObject> listHouse = new List<GameObject>();


    public void OnClickDown()
    {
        LogController.Log("house click down" + isCreatale);
        if(isCreatale && isActive.isOn)
        {
            GameObject house = Instantiate(housePrefab, CustomCursor.currentHit.point, Quaternion.identity);
            listHouse.Add(house);
            isCreatale = false;
        }
    }



    public void OnClickUp()
    {
    }

    private void Update()
    {
        
    }

}
