using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonOnClick : MonoBehaviour, CursorEvent {
    
    Button button;
    bool isClickable;
    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnClickDown()
    {
        isClickable = true;
        
    }

    public void OnClickUp()
    {
    }

	void Update () {
		if(CustomCursor.HitTransform(transform))
        {
            Debug.Log("hit " + gameObject.name);
            button.Select();
        }

        if(isClickable)
        {
            button.onClick.Invoke();
            isClickable = false;
        }
        //else
        //{
        //    EventSystem.current.SetSelectedGameObject(null);
        //}
	}
}
