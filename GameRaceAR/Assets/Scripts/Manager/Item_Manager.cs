using UnityEngine;

public class Item_Manager : MonoBehaviour {

    private float m_valueDefail = 5f;
    private int m_priceDefail = 5;
    private string m_typeDefail = "Engine";
    private int m_isbuyDefail = 0;
    private int m_countItem;

    private void Start() { 
        m_countItem = Database_Controller.DB.m_countItem;
    }

    public void f_InsertItem(string name)
    {
            Item item = new Item(++m_countItem, name, m_valueDefail, m_priceDefail, m_typeDefail, m_isbuyDefail);
            l_Item.l_item.Add(item);
            Database_Controller.DB.m_User_DB.InsertUser(item);
            return;
    }
}
