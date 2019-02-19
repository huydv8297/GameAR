using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour {
    public static RaycastHit currentHit;
    public static RaycastHit[] hits;
    static CursorEvent buttonOnClick;
    public static bool isClick;
    
    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Debug.DrawRay(ray.origin, ray.direction * 50000, Color.red, Mathf.Infinity);
        hits = Physics.RaycastAll(ray);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            currentHit = hit;
            
            buttonOnClick = currentHit.transform.GetComponent<CursorEvent>();
        }
    }

    public static void OnMouseClickDown()
    {
        if(buttonOnClick != null)
            buttonOnClick.OnClickDown();
        isClick = true;
    }

    public static void OnMouseClickUp()
    {
        if (buttonOnClick != null)
            buttonOnClick.OnClickUp();

        isClick = false;
    }

    public static bool HitTransform(Transform transform)
    {
        if (currentHit.transform != null && currentHit.transform == transform)
            return true;
        return false;
    }

    public static bool HitAllTransform(Transform transform)
    {
        if (hits == null)
            return false;
        foreach(var hit in hits)
        {
            if (hit.transform == transform)
                return true;
        }
        return false;
    }
}
