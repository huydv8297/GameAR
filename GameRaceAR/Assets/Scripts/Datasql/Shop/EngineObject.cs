using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EngineObject : MonoBehaviour {

    public Text Enginevalue;
    public Button Engineprice;
    public Transform EngineParent;

    private int isBuy;
    private string EngineName;

    private void Start()
    {
        setTransform();
    }

    public void SetEngine(string enginename, GameObject item, float enginevalue, float engineprice, int isbuy)
    {
        this.EngineName = enginename;
        this.Engineprice.GetComponentInChildren<Text>().text = engineprice.ToString();
        this.Enginevalue.text = enginevalue.ToString();
        this.isBuy = isbuy;
        GameObject tmp_engine = Instantiate(item);

        if(isbuy == 1)
        {
            Engineprice.GetComponent<Button>().interactable = false;
        }

        tmp_engine.transform.SetParent(EngineParent, false);
    }

    public string BuyEngine()
    {
        int price = int.Parse(Engineprice.GetComponentInChildren<Text>().text);
        int coin = l_User.l_user.GetCurrentUser().Coin;
        if (coin >= price && isBuy == 0)
        {
            Engineprice.GetComponent<Button>().interactable = false;
            l_User.l_user.UpdateCoin(price);
            FindObjectOfType<Engine_Manager>().updateCoin();
            //l_Car.l_car.addPara(l_Car.l_car.CheckCarInList(FindObjectOfType<CarObject>().CarName.text).CarId - 1, l_Item.l_item.CheckItemInList(EngineName).Typeengine, l_Item.l_item.CheckItemInList(EngineName).Value);
            Database_Controller.DB.m_User_DB.Update("Item", "Isbuy", "1", "Itemname", EngineName);
            Database_Controller.DB.updateUpgrade();
            FindObjectOfType<CarObject>().setPara();
            return EngineName;
        }
        return null;
    }

    public void Buy()
    {
        Debug.Log("Buy " + EngineName);
        l_Item.l_item.Buy(EngineName);
        BuyEngine();
    }

    private void setTransform()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localRotation = new Quaternion(0, 0, 0, 0);
    }
}
