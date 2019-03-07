using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House {
    public string idMapH { get; set; }

    public string idHouseH { get; set; }

    public House(string idmapH, string idhouseH)
    {
        this.idMapH = idmapH;
        this.idHouseH = idhouseH;
    }

}
public class PointHouse
{
    public string idHouseP { get; set; }

    public string HposxP { get; set; }

    public string HposyP { get; set; }

    public string HposzP { get; set; }

    public string HdirxP { get; set; }

    public string HdiryP { get; set; }

    public string HdirzP { get; set; }

    public PointHouse(string idhouseP, string hposxP, string hposyP, string hposzP, string hdirxP, string hdiryP, string hdirzP)
    {
        this.idHouseP = idhouseP;
        this.HposxP = hposxP;
        this.HposyP = hposyP;
        this.HposzP = hposzP;
        this.HdirxP = hdirxP;
        this.HdiryP = hdiryP;
        this.HdirzP = hdirzP;

    }


}
