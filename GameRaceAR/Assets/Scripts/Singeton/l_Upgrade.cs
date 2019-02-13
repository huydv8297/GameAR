using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class l_Upgrade : MonoBehaviour
{

    #region Singeton
    public static l_Upgrade l_upgrade;

    private void Awake()
    {
        if (l_upgrade != null)
        {
            Debug.LogWarning("More than one List Upgrade !");
            return;
        }
        l_upgrade = this;
    }
    #endregion

    public List<Upgrade> Upgrades = new List<Upgrade>();

    public void Add(Upgrade upgrade)
    {
        Upgrades.Add(upgrade);
        return;
    }

    public void Remove(int carid)
    {
        Upgrade result = Upgrades.Find(x => x.CarId == carid);
        if (result != null)
        {
            Upgrades.Remove(result);
        }
        return;
    }

    public Upgrade CheckUpgradeInList(int id)
    {
        Upgrade result = Upgrades.Find(x => x.CarId == id);
        if (result == null)
        {
            return null;
        }
        return result;
    }

    public void ToConsole()
    {
        foreach (var upgrade in Upgrades)
        {
            string tmp = upgrade.ToString();
            Debug.Log(tmp);
        }
    }
}
