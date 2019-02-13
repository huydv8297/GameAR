using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car_Manager : MonoBehaviour {

    private int m_coinDefail = 10;
    private float m_speedDefail = 10f;
    private float m_stiffnessDefail = 10f;
    private float m_maxspeedDefail = 10f;
    private int m_isbuyDefail = 0;
    private int m_countCar;

    private void Start()
    {
        m_countCar = Database_Controller.DB.m_countCar;
    }

    public void f_InsertCar(string name)
    {
        Car car = new Car(++m_countCar, name, m_coinDefail, m_speedDefail, m_stiffnessDefail, m_maxspeedDefail, m_isbuyDefail);
        l_Car.l_car.Add(car);
        Database_Controller.DB.m_User_DB.InsertUser(car);
        return;
    }

    public void change_Scene()
    {
        SceneManager.LoadScene("Menu");
    }
}
