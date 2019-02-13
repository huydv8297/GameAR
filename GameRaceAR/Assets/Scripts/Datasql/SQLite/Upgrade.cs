using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Upgrade {

    public int CarId { get; set; }
    public int[] Items { get; set; }

    public Upgrade (int carid, string items)
    {
        this.CarId = carid;
        this.Items = SplitString(items);
    }

    public override string ToString()
    {
        return string.Format("[Car Id : {0}, Item : {1}]", this.CarId, itemToString(this.Items));
    }

    private int[] SplitString(string str)
    {
        return Array.ConvertAll(str.Split('.'), int.Parse);
    }

    private string itemToString(int[] integer)
    {
        string result = "";
        for(int i = 0; i < integer.Length; i++)
        {
            result += integer[i] + " ";
        }
        return result;
    }
}
