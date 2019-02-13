using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelect : MonoBehaviour {

    public GameObject Car1;
    public GameObject Car2;
    public GameObject Car3;

    public int CarSelected;

    private void Start()
    {
        Car1.SetActive(true);
        Car2.SetActive(false);
        Car3.SetActive(false);
        
        CarSelected = 1;
    }

    public void LoadCar1()
    {
        Car1.SetActive(true);
        Car2.SetActive(false);
        Car3.SetActive(false);

        CarSelected = 1;
    }

    public void LoadCar2()
    {
        Car2.SetActive(true);
        Car1.SetActive(false);
        Car3.SetActive(false);

        CarSelected = 2;
    }

    public void LoadCar3()
    {
        Car3.SetActive(true);
        Car1.SetActive(false);
        Car2.SetActive(false);

        CarSelected = 3;
    }
}
