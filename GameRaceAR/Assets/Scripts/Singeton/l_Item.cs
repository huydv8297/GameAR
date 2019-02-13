using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class l_Item : MonoBehaviour
{

    #region Singeton
    public static l_Item l_item;

    private void Awake()
    {
        if (l_item != null)
        {
            Debug.LogWarning("More than one List Item !");
            return;
        }
        l_item = this;
    }
    #endregion

    public List<Item> Items = new List<Item>();

    public void Add(Item item)
    {
        Items.Add(item);
        return;
    }

    public void Remove(string itemname)
    {
        Item result = Items.Find(x => x.Itemname == itemname);
        if (result != null)
        {
            Items.Remove(result);
        }
        return;
    }

    public void Remove(int itemid)
    {
        Item result = Items.Find(x => x.ItemId == itemid);
        if (result != null)
        {
            Items.Remove(result);
        }
        return;
    }

    public void ToConsole()
    {
        foreach (var item in Items)
        {
            string tmp = item.ToString();
            Debug.Log(tmp);
        }
    }

    public Item CheckItemInList(string itemname)
    {
        var result = Items.Find(x => x.Itemname == itemname);
        if (result == null)
        {
            return null;
        }
        return result;
    }

    public Item CheckItemInList(int itemid)
    {
        var result = Items.Find(x => x.ItemId == itemid);
        if (result == null)
        {
            return null;
        }
        return result;
    }

    public void Buy(string enginename)
    {
        foreach (Item tmp in Items)
        {
            if (tmp.Itemname == enginename)
            {
                tmp.IsBuy = 1;
                return;
            }
        }
    }
}
