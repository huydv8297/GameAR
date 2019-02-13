using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine_Manager : MonoBehaviour {

    public GameObject m_enginePrefab;
    public GameObject m_carPrefab;

    public Transform m_ListCar;
    public Transform m_listParent;
    public Text m_UsernameText;
    public Text m_UserCoin;
    public int m_maxItem = 4;

    [HideInInspector]
    public int currentCarInTable = 1;

    private void Start()
    {
        m_UsernameText.text = l_User.l_user.Users[0].Username;
        updateCoin();
        showItemInShop();
    }

    /*private void f_Showitem()
    {
        for (int i = 0; i < Math.Min(m_maxItem, m_engineObjec.Length); i++)
        {
            GameObject tmpObjec = Instantiate(m_enginePrefab);
            tmpObjec.GetComponent<EngineObject>().SetEngine(m_engineObjec[i].typeengine.ToString(), m_engineObjec[i].name, m_engineObjec[i].engin, m_engineObjec[i].value, m_engineObjec[i].price, m_engineObjec[i].isDefaultEngine);
            Player.player.Add(m_engineObjec[i]);
            tmpObjec.transform.SetParent(m_listParent, false);
        }
    }*/


//    Upgrade result = l_Upgrade.l_upgrade.CheckUpgradeInList(l_Car.l_car.CheckCarInList(name).CarId);
//        if (result.Items != null)
//        {
//            for(int i = 0; i<result.Items.Length; i++)
//            {
//                Item item_sesult = l_Item.l_item.CheckItemInList(result.Items[i]);

//}
//        }
    public void showItemInShop()
    {
        int carid = currentCarInTable;
        clearParent(m_ListCar);
        clearParent(m_listParent);
        Car carresult = l_Car.l_car.CheckCarInList(carid);
        Upgrade upgraderesult = l_Upgrade.l_upgrade.CheckUpgradeInList(carid);

        GameObject tmpCarObject = Instantiate(m_carPrefab);
        tmpCarObject.GetComponent<CarObject>().SetCar(carresult.Carname, Database_Controller.DB.m_carObject[carid - 1], carresult.Speed, carresult.Stiffness, carresult.Maxspeed, carresult.Coin, carresult.IsBuy);
        tmpCarObject.transform.SetParent(m_ListCar);

        foreach (int a in upgraderesult.Items)
        {
            Item itemresult = l_Item.l_item.CheckItemInList(a);
            GameObject tmpItemObject = Instantiate(m_enginePrefab);
            tmpItemObject.GetComponent<EngineObject>().SetEngine(itemresult.Itemname, Database_Controller.DB.m_engineObjec[a - 1], itemresult.Value, itemresult.Price, itemresult.IsBuy);
            tmpItemObject.transform.SetParent(m_listParent);
        }
    }

    public void updateCoin()
    {
        m_UserCoin.text = l_User.l_user.Users[0].Coin.ToString();
    }

    private void clearParent(Transform parent)
    {
        int childs = parent.childCount;
        if(childs == 0)
        {
            return;
        }
        for(int i = 0; i < childs; i++)
        {
            GameObject.Destroy(parent.GetChild(i).gameObject);
        }
    }

    public void leftCurrentCar()
    {
        int lefscar = currentCarInTable - 1;
        if(lefscar < 1)
        {
            return;
        }
        else
        {
            currentCarInTable--;
            showItemInShop();
        }
    }

    public void rightCurrentCar()
    {
        int rightCar = currentCarInTable + 1;
        if (rightCar > l_Car.l_car.Cars.Count)
        {
            return;
        }
        else
        {
            currentCarInTable++;
            showItemInShop();
        }
    }
}
