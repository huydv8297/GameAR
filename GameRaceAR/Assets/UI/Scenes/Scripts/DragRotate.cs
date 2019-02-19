using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotate : MonoBehaviour {
    float f_lastX = 0f;
    float f_difX = 0.5f;
    int i_direction = 5;


    void Update() {
        if (Input.GetMouseButton(0))
        {
            f_difX = 0.0f;
        }
        else if (Input.GetMouseButton(0))
        {
            f_difX = Mathf.Abs(f_lastX - Input.GetAxis("Mouse X"));
        
        if (f_lastX < Input.GetAxis("Mouse X"))
        {
            i_direction = 5;
            transform.Rotate(Vector3.up, -f_difX);
        }
        if (f_lastX > Input.GetAxis("Mouse X"))
        {
            i_direction = 5;
            transform.Rotate(Vector3.up, f_difX);
        }
        f_lastX = -Input.GetAxis("Mouse X");
        }
        else
        {
            if (f_difX > 0.5f) f_difX -= 0.05f;
            if (f_difX < 0.5f) f_difX += 0.05f;

            transform.Rotate(Vector3.up, f_difX * i_direction);
        }
    }
        
}
