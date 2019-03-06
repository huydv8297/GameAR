using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public Transform target;
    public Transform cursor;
    public float fixedY;
    public float speed = 20f;

    public bool isMoveable;
	void Start () {
        fixedY = transform.position.y;
	}

    private void Update()
    {
        if(CustomCursor.HitAllTransform(transform))
        {
            isMoveable = false;
        }
        else
        {
            isMoveable = true;
        }
    }

    void LateUpdate () {
        Quaternion newRotation = target.rotation;
        newRotation.x = 0;
        newRotation.z = 0;

        transform.rotation = newRotation;
        if (isMoveable)
        {
            StartCoroutine(ChangePosition());
            isMoveable = false;
        }       
    }

    IEnumerator ChangePosition()
    {
        float percent = 0f;
        Vector3 oldPosition = transform.position;
        while(percent <= 1)
        {
            percent += Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(oldPosition, target.position, percent);
            
            newPosition.y = fixedY;
            transform.position = newPosition;
            yield return new WaitForSeconds(Time.deltaTime / speed);
        }
    }

  
}
