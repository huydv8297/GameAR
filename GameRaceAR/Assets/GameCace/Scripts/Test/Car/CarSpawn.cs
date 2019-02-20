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

    public void OnClickDown()
    {
       if(flagContainer.childCount != 0)
        {
            postionSpawn = flagContainer.transform.GetChild(0).position;
            //GameObject player = Instantiate(playerPrefab, postionSpawn, Quaternion.identity);
            //player.transform.parent = transform;
            GameObject npc = Instantiate(npcPrefab, postionSpawn, Quaternion.identity);
            npc.transform.parent = transform;
            npc.transform.LookAt(flagContainer.transform.GetChild(1));
            npc.GetComponent<AICarTest>().pathground = flagContainer;
            npc.GetComponent<AICarTest>().Play();
            LogController.log.text = "Instance car";

            GameObject player = Instantiate(playerPrefab, postionSpawn + new Vector3(0.5f, 0f, 0.5f), Quaternion.identity);
            player.transform.parent = transform;
            npc.transform.LookAt(flagContainer.transform.GetChild(1));
        }
    }

    private void Update()
    {
        
    }
}