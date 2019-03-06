using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House {
    public int idMapH { get; set; }

    public int idHouseH { get; set; }

    public House(int idmapH, int idhouseH)
    {
        this.idMapH = idmapH;
        this.idHouseH = idhouseH;
    }

}
public class PointHouse
{
    public int idHouseP { get; set; }

    public int idPountHouseP { get; set; }

    public string HposxP { get; set; }

    public string HposyP { get; set; }

    public string HposzP { get; set; }

    public string HdirxP { get; set; }

    public string HdiryP { get; set; }

    public string HdirzP { get; set; }

    public PointHouse(int idhouseP, int idpounthouseP, string hposxP, string hposyP, string hposzP, string hdirxP, string hdiryP, string hdirzP)
    {
        this.idHouseP = idhouseP;
        this.idPountHouseP = idpounthouseP;
        this.HposxP = hposxP;
        this.HposyP = hposyP;
        this.HposzP = hposzP;
        this.HdirxP = hdirxP;
        this.HdiryP = hdiryP;
        this.HdirzP = hdirzP;

    }


}
