using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class l_Car : MonoBehaviour {

    #region Singeton
    public static l_Car l_car;

    private void Awake()
    {
        if (l_car != null)
        {
            Debug.LogWarning("More than one List Car !");
            return;
        }
        l_car = this;
    }
    #endregion

    public List<Car> Cars = new List<Car>();

    public void Add(Car car)
    {
        Cars.Add(car);
        return;
    }

    public void Remove(string carname)
    {
        Car result = Cars.Find(x => x.Carname == carname);
        if (result != null)
        {
            Cars.Remove(result);
        }
        return;
    }

    public void Remove(int carid)
    {
        Car result = Cars.Find(x => x.CarId == carid);
        if (result != null)
        {
            Cars.Remove(result);
        }
        return;
    }

    public void ToConsole()
    {
        foreach (var car in Cars)
        {
            string tmp = car.ToString();
            Debug.Log(tmp);
        }
    }

    public Car CheckCarInList(string carname)
    {
        var result = Cars.Find(x => x.Carname == carname);
        if (result == null)
        {
            return null;
        }
        return result;
    }

    public Car CheckCarInList(int carid)
    {
        var result = Cars.Find(x => x.CarId == carid);
        if (result == null)
        {
            return null;
        }
        return result;
    }

    public void Buy(string carname)
    {
        foreach (Car tmp in Cars)
        {
            if (tmp.Carname == carname)
            {
                tmp.IsBuy = 1;
                return;
            }
        }
    }

    public void addPara(int carid, string type, float value)
    {
        if (type == "Speed")
        {
            Cars[carid].Speed += value;
        }
        else if (type == "Stiffness")
        {
            Cars[carid].Stiffness += value;
        }
        else if (type == "Maxspeed")
        {
            Cars[carid].Maxspeed += value;
        }
    }
}
