using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{

    public List<GameObject> flags = new List<GameObject>();
    public GameObject flagPrefab;
    public GameObject curentFlag;
    public Toggle isCreate;
    public bool isSetable;

    public void OnClickDown()
    {
        if (isCreate.isOn && isSetable)
        {
            Debug.Log("Add flag");
            LogController.log.text = "Add flag";
            flags.Add(curentFlag);
            curentFlag = null;
        }
        else
        {
            LogController.log.text = "else Add flag";
            curentFlag = null;
        }
    }


    private void Update()
    {
        if(CustomCursor.isClick)
        {
            OnClickDown();
            CustomCursor.isClick = false;
        }

        if (!isCreate.isOn)
        {
            if (curentFlag != null)
                Destroy(curentFlag);
            curentFlag = null;
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red, Mathf.Infinity);
            if (curentFlag == null)
            {
                curentFlag = Instantiate(flagPrefab, transform);
                curentFlag.GetComponent<CheckArea>().SetRedStatus();
            }
            else
            {
                Vector3 newPos = hit.point;
                curentFlag.transform.position = newPos + new Vector3(0, 1f, 0);
            }

            if (hit.transform.CompareTag("road"))
            {
                isSetable = true;
                curentFlag.GetComponent<CheckArea>().SetGreenStatus();
            }
            else if (hit.transform.CompareTag("terria"))
            {
                isSetable = false;
                curentFlag.GetComponent<CheckArea>().SetRedStatus();
            }
        }
    }
}
