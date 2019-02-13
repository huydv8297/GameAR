using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Car {  

	public int CarId { get; set; }
    public string Carname { get; set; }
    public int Coin { get; set; }
    public float Speed { get; set; }
    public float Stiffness { get; set; }
    public float Maxspeed { get; set; }
    public int IsBuy { get; set; }

    public Car(int carid, string carname, int coin, float speed, float stiffness, float maxspeed, int isbuy)
    {
        this.CarId = carid;
        this.Carname = carname;
        this.Coin = coin;
        this.Speed = speed;
        this.Stiffness = stiffness;
        this.Maxspeed = maxspeed;
        this.IsBuy = isbuy;
    }

    public override string ToString()
    {
        return string.Format("[Car Id : {0}, Car Name : {1}, Coin : {5}, Speed : {2}, Stiffness : {3}, Max Speed : {4}]", this.CarId, this.Carname, this.Speed, this.Stiffness, this.Maxspeed, this.Coin);
    }
}
