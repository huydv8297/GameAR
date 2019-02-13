using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarObject : MonoBehaviour {

    public Text CarName;
    public Text CarSpeed;
    public Text CarStiffiness;
    public Text CarMaxSpeed;
    public Button CarPrice;

    public Transform CarParent;

    private string carname;
    private int isBuy;

    private void Start()
    {
        setTransform();
    }

    public void SetCar(string name, GameObject carobject, float speed, float stiffiness, float maxspeed, int price, int isbuy)
    {
        //setTransform();

        this.CarName.text = name;
        this.CarSpeed.text = speed.ToString();
        this.CarStiffiness.text = stiffiness.ToString();
        this.CarMaxSpeed.text = maxspeed.ToString();
        this.CarPrice.GetComponentInChildren<Text>().text = price.ToString();
        this.isBuy = isbuy;

        GameObject tmp_car = Instantiate(carobject);
        if(isbuy == 1)
        {
            CarPrice.GetComponent<Button>().interactable = false;
        }

        tmp_car.transform.SetParent(CarParent, false);
    }

    public string BuyCar()
    {
        int price = int.Parse(CarPrice.GetComponentInChildren<Text>().text);
        int coin = l_User.l_user.GetCurrentUser().Coin;
        if(coin >= price && isBuy == 0)
        {
            CarPrice.GetComponent<Button>().interactable = false;
            l_User.l_user.UpdateCoin(price);
            FindObjectOfType<Engine_Manager>().updateCoin();
            Database_Controller.DB.m_User_DB.Update("Car", "Isbuy", "1", "Carname", CarName.text);
            return CarName.text;
        }
        return null;
    }

    public void Buy()
    {
        Debug.Log("Buy " + CarName.text);
        BuyCar();
        l_Car.l_car.Buy(CarName.text);
    }

    private void setTransform()
    {
        transform.localPosition = new Vector3(0, 0, 0.1f);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    public void setPara()
    {
        Car result = l_Car.l_car.CheckCarInList(CarName.text);
        CarSpeed.text = result.Speed.ToString();
        CarStiffiness.text = result.Stiffness.ToString();
        CarMaxSpeed.text = result.Maxspeed.ToString();
    }
}
