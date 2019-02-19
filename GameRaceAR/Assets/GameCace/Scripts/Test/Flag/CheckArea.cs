using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour {

    public MeshRenderer checkArea;
    public Material green;
    public Material red;
    private Material curenrMaterial;
    void Start () {
        curenrMaterial = green;
    }
	
	// Update is called once per frame
	void Update () {
        checkArea.material = curenrMaterial;
    }

    public void SetGreenStatus()
    {
        curenrMaterial = green;
    }

    public void SetRedStatus()
    {
        curenrMaterial = red;
    }
}
