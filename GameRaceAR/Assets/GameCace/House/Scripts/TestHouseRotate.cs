using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHouseRotate : MonoBehaviour, CursorEvent
{
    //Rotate

    float xDeg;
    float yDeg;
    public float speed;
    public float leftSpeed;
    //Move or drag
    Vector3 Distance;
    float PositionX;
    float PositionY;
    public GameObject controlPanel;
    //void Update()
    //{
    //    if (CustomCursor.HitTransform(transform) && CustomCursor.isClick)
    //    {
    //        CustomCursor.isClick = false;
            
    //    };
    //}

    public void Rotation()
    { 
            xDeg -= speed;
            Quaternion fromRotation = transform.rotation;
            Quaternion toRotation = Quaternion.Euler(yDeg, xDeg, 0);
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * leftSpeed);
    }

    public void OnClickX()
    {
        Destroy(gameObject.transform.parent);
    }

  
    // move or drag a game object
    private void OnMouseDown()
    {
        Distance = Camera.main.WorldToScreenPoint(transform.position);
        PositionX = Input.mousePosition.x - Distance.x;
        PositionY = Input.mousePosition.y - Distance.y;
    }
    private void OnMouseDrag()
    {
        Vector3 CurrentPosition = new Vector3(Input.mousePosition.x - PositionX, Input.mousePosition.y - PositionY, Distance.z);
        Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(CurrentPosition);
        transform.position = WorldPosition;
    }

    public void OnClickDown()
    {
        controlPanel.SetActive(true);
    }

    public void OnClickUp()
    {
    }

}
