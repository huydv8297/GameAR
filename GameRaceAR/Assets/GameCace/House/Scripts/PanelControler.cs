using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControler : MonoBehaviour {

    public GameObject house;
    public HouseSpawn spawn;
	void Start () {
        //spawn = GameObject.FindGameObjectWithTag("terria").GetComponent<HouseSpawn>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180,0));
    }

    public void Delete()
    {
        Destroy(transform.parent.gameObject);
        Debug.Log("Delete");
    }

    public void Complete()
    {
        spawn.isCreatale = true;
        gameObject.SetActive(false);
        Debug.Log("Complete");
    }
}
