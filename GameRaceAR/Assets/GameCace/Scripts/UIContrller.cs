using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContrller : MonoBehaviour {

    public float fixedY;
	void Start () {
        fixedY = transform.position.y;
	}
	
	void Update () {

        Vector3 newPost = transform.position;
        newPost.y = fixedY;
        transform.position = newPost;
    }
}
