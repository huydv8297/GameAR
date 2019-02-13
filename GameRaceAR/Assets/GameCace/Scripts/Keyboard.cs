using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour {

    public SimpleCarController car;

    public void Up(int param)
    {
        car.Accelerateduong(param);
        Debug.Log("Tunnnnnnnnnn");
    }

    public void Down(int param)
    {
        car.Accelerateam(param);
    }

    public void Left(int param)
    {
        car.steerduong(param);
    }

    public void Right(int param)
    {
        car.steeram(param);
    }

}
