using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

	public List<GameObject> flags = new List<GameObject>();
    public GameObject flagPrefab;
    public GameObject curentFlag;

    public bool isCreate;

    private void Update()
    {

        if (!isCreate)
            return;

        if(Input.GetKeyDown(KeyCode.A))
        {
            flags.Add(curentFlag);
            curentFlag = null;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            curentFlag = null;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("flag");
            
            Debug.DrawRay(ray.origin , ray.direction * Mathf.Infinity, Color.red, Mathf.Infinity);
            if(curentFlag == null)
            {
                curentFlag = Instantiate(flagPrefab, transform);
     
            }else
            {
                Vector3 newPos = hit.point;
                newPos.y += 1f;
                curentFlag.transform.position = newPos;
            }

            if(hit.transform.CompareTag("road"))
            {
                curentFlag.GetComponent<CheckArea>().SetGreenStatus();
            }
            else if (hit.transform.CompareTag("terria"))
            {
                curentFlag.GetComponent<CheckArea>().SetRedStatus();
            }

            
        }
            
    }

}
