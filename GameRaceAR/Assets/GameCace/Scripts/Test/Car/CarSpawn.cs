using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSpawn : MonoBehaviour
{

    public Transform flagContainer;
    public GameObject playerPrefab;
    public GameObject npcPrefab;
    public GameObject policePrefab;
    public Vector3 postionSpawn;

    public float space;
    int count;
    float width;
    public void OnClickDown()
    {
       if(flagContainer.childCount != 0)
        {
            transform.LookAt(flagContainer.transform.GetChild(1).position);
            postionSpawn = flagContainer.transform.GetChild(0).position;

            GameObject npc = Instantiate(npcPrefab, postionSpawn, Quaternion.identity);
            npc.transform.parent = transform;
            npc.GetComponent<AICarTest>().pathground = flagContainer;
            npc.GetComponent<AICarTest>().Play();
            
            GameObject player = Instantiate(playerPrefab, postionSpawn, Quaternion.identity);
            player.transform.parent = transform;

            //npc.transform.LookAt(flagContainer.transform.GetChild(1));
            //player.transform.LookAt(flagContainer.transform.GetChild(1));

            SpawnCar(npc);
            SpawnCar(player);
            LogController.log.text = "Instance car";
        }
    }

    public void SpawnCar(GameObject car)
    {
        count++;
        car.transform.rotation = transform.rotation;
        car.transform.position +=  new Vector3(width + space * count, 0, 0);
        width += car.GetComponent<BoxCollider>().bounds.size.x;
    }
}