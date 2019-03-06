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
        if (isCreate.isOn)
        {
            Debug.Log("Add flag");
            LogController.log.text = "Add flag";
            //flags.Add(curentFlag);
            if (isSetable)
            {
                curentFlag = Instantiate(flagPrefab, transform);
                curentFlag.GetComponent<CheckArea>().SetRedStatus();
            }
        }
        else 
        {
            LogController.log.text = "else Add flag";
            isSetable = false;
            
            curentFlag = null;
            return;
        }
    }

    private void Update()
    {
        if (CustomCursor.isClick)
        {
            OnClickDown();
            CustomCursor.isClick = false;
        }

        if (!isCreate.isOn)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red, Mathf.Infinity);

            if (hit.transform.CompareTag("road"))
            {
                isSetable = true;
                curentFlag.GetComponent<CheckArea>().SetGreenStatus();
            }
            else
            {
                isSetable = false;
                curentFlag.GetComponent<CheckArea>().SetRedStatus();
            }
            
            if (curentFlag != null)
            {
                Vector3 newPos = hit.point;
                curentFlag.transform.position = newPos + new Vector3(0, 1f, 0);
            }
        }
    }

    public void OnFlagDisalbe()
    {
        if (!isCreate.isOn && curentFlag != null)
            Destroy(curentFlag);
    }
}
