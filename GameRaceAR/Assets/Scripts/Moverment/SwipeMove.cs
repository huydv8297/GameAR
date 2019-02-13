using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMove : MonoBehaviour {

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private float distance = 450f;
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if(endTouchPosition.x - startTouchPosition.x > distance)
            {
                FindObjectOfType<Engine_Manager>().leftCurrentCar();
            }
            if(startTouchPosition.x - endTouchPosition.x> distance)
            {
                FindObjectOfType<Engine_Manager>().rightCurrentCar();
            }
        }
	}
}
