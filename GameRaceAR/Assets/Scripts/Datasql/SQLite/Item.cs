using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Item {

    public int ItemId { get; set; }
    public string Itemname { get; set; }
    public int Price { get; set; }
    public float Value { get; set; }
    public string Typeengine { get; set; }
    public int IsBuy { get; set; }

    public Item(int itemid, string itemname, float value, int price, string typeengine, int isbuy)
    {
        this.ItemId = itemid;
        this.Itemname = itemname;
        this.Value = value;
        this.Price = price;
        this.Typeengine = typeengine;
        this.IsBuy = isbuy;
    }

    public override string ToString()
    {
        return string.Format("[Item Id : {0}, Item Name : {1}, Price : {2}, Value : {3}, Type : {4}], Is Buy : {5}", this.ItemId, this.Itemname, this.Price, this.Value, this.Typeengine, this.IsBuy);
    }
}
