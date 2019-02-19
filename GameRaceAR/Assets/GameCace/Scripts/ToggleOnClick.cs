using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleOnClick : MonoBehaviour, CursorEvent
{

    Toggle toggle;
    bool isClickable;
    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    public void OnClickDown()
    {
        isClickable = true;
        
    }

    public void OnClickUp()
    {
    }

    void Update()
    {
        if (CustomCursor.HitTransform(transform))
        {
            Debug.Log("hit " + gameObject.name);
            toggle.Select();
        }

        if(isClickable)
        {
            toggle.isOn = !toggle.isOn;
            isClickable = false;
        }
        //else
        //{
        //    EventSystem.current.SetSelectedGameObject(null);
        //}
    }
}
