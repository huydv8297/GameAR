using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car  {

    public int idCar { get; set; }

    public int idColorC { get; set; }

    public int idBrandC { get; set; }

    public int idModelC { get; set; }

   // public string SpeedC { get; set; }

    public Car(int idcar, int idcolorC, int idbrandC, int idmodelC)
    {
        this.idCar = idcar;
        this.idColorC = idcolorC;
        this.idBrandC = idbrandC;
        this.idModelC = idmodelC;
       // this.SpeedC = speedC;
    }

}
public class BrandDB
{
    public int idBrandB { get; set; }

    public string BrandB { get; set; }

    public BrandDB(int idbrandB, string brandB)
    {
        this.idBrandB = idbrandB;
        this.BrandB = brandB;
    }
}
public class ColorDB
{
    public int idColorC { get; set; }

    public string ColorC { get; set; }

    public string PaintC { get; set; }

    public ColorDB(int idcolorC, string colorC, string paintC)
    {
        this.idColorC = idcolorC;
        this.ColorC = colorC;
        this.PaintC = paintC;
    }
}
public class ModelDB
{
    public int idModelM { get; set; }

    public string ModelM { get; set; }

    public ModelDB(int idmodelM, string modelM)
    {
        this.idModelM = idmodelM;
        this.ModelM = modelM;
    }
}

