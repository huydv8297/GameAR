using UnityEngine;

public class Upgrade_Manager : MonoBehaviour {

    private int m_itemDefail = 1;

    public void f_InsertUpgrade(int carid)
    {
        Upgrade upgrade = new Upgrade(carid, m_itemDefail.ToString());
        l_Upgrade.l_upgrade.Add(upgrade);
        Database_Controller.DB.m_User_DB.InsertUser(upgrade);
        return;
    }

}
