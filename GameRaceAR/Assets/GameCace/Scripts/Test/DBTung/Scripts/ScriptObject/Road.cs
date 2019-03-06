using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road {
    public int idMapR { get; set; }

    public int idRoadR { get; set; }

    public Road(int idmapR, int idroadR)
    {
        this.idMapR = idmapR;
        this.idRoadR = idroadR;
    }

}
public class PointRoad
{
    public int idRoadP { get; set; }

    public int idPointRoadP { get; set; }

    public string RposxP { get; set; }

    public string RposyP { get; set; }

    public string RposzP { get; set; }

    public string RdirxP { get; set; }

    public string RdiryP { get; set; }

    public string RdirzP { get; set; }

    public PointRoad(int idroadP, int idpointroadP, string rposxP, string rposyP, string rposzP, string rdirxP, string rdiryP, string rdirzP)
    {
        this.idRoadP = idroadP;
        this.idPointRoadP = idpointroadP;
        this.RposxP = rposxP;
        this.RposyP = rposyP;
        this.RposzP = rposzP;
        this.RdirxP = rdirxP;
        this.RdiryP = rdiryP;
        this.RdirzP = rdirzP;

    }
}
