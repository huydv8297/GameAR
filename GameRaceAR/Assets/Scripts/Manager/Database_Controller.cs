using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database_Controller : MonoBehaviour {

    #region Singeton
    public static Database_Controller DB;
    private void Awake()
    {
        if(DB != null)
        {
            Debug.Log("More than one DB !");
            return;
        }
        DB = this;
    }
    #endregion 

    private string m_databaseName = "Unity_Car_DB.db";
    private string[] m_tableName = { "Car", "Item", "User", "Upgrade" };
    public Database_Connection m_User_DB;
    public GameObject[] m_carObject;
    public GameObject[] m_engineObjec;
    [HideInInspector]
    public int m_countCar;
    [HideInInspector]
    public int m_countItem;

    public delegate void PrintDelegate();
    public event PrintDelegate printEvent;

    private void Start()
    {
        m_User_DB = new Database_Connection(m_databaseName);
        m_countCar = m_User_DB.selectTable(m_databaseName, m_tableName[0]);
        m_countItem = m_User_DB.selectTable(m_databaseName, m_tableName[1]);
        m_User_DB.selectTable(m_databaseName, m_tableName[2]);
        m_User_DB.selectTable(m_databaseName, m_tableName[3]);

        updateItem();
        updateCar();
        FixUpgrade();
        updateUpgrade();

        printEvent += f_PrintCar;
        printEvent += f_PrintUser;
        printEvent += f_PrintItem;
        printEvent += f_PrintUpgrade;
    }

    public void RemoveTable(string table, string colunm, string value)
    {
        m_User_DB.Remove(table, colunm, value);
    }

    public void Print()
    {
        if(printEvent != null)
        {
            printEvent();
        }
    }

    public void f_PrintCar()
    {
        Debug.Log("--------Print Car !--------");
        l_Car.l_car.ToConsole();
        Debug.Log("----------------------------");
    }

    public void f_PrintItem()
    {
        Debug.Log("--------Print Item !--------");
        l_Item.l_item.ToConsole();
        Debug.Log("----------------------------");
    }

    public void f_PrintUser()
    {
        Debug.Log("--------print user !--------");
        l_User.l_user.ToConsole();
        Debug.Log("----------------------------");
    }

    public void f_PrintUpgrade()
    {
        Debug.Log("--------Print Upgrade !--------");
        l_Upgrade.l_upgrade.ToConsole();
        Debug.Log("----------------------------");
    }

    public void updateCar()
    {
        foreach (GameObject a in m_carObject)
        {
            Car tmpCar = l_Car.l_car.CheckCarInList(a.name);
            if (tmpCar == null)
            {
                FindObjectOfType<Car_Manager>().f_InsertCar(a.name);
            }
        }
    }

    public void updateItem()
    {
        foreach (GameObject a in m_engineObjec)
        {
            Item tmpItem = l_Item.l_item.CheckItemInList(a.name);
            if (tmpItem == null)
            {
                FindObjectOfType<Item_Manager>().f_InsertItem(a.name);
            }
        }
    }

    public void updateUpgrade()
    {
        foreach (Upgrade a in l_Upgrade.l_upgrade.Upgrades)
        {
            foreach (int b in a.Items)
            {
                Item tmpItem = l_Item.l_item.CheckItemInList(b);
                if (tmpItem.IsBuy == 1)
                {
                    l_Car.l_car.addPara(a.CarId - 1, tmpItem.Typeengine, tmpItem.Value);
                }
            }
        }
    }

    public void FixUpgrade()
    {
        foreach (GameObject a in m_carObject)
        {
            int carid = l_Car.l_car.CheckCarInList(a.name).CarId;
            Upgrade tmpUpgrade = l_Upgrade.l_upgrade.CheckUpgradeInList(carid);
            if (tmpUpgrade == null)
            {
                FindObjectOfType<Upgrade_Manager>().f_InsertUpgrade(carid);
            }
        }
    }
}
