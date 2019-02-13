using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Location
{
    public int TrackId { get; set; }

    public int Serial { get; set; }

    public float X1 { get; set; }
    public float Y1 { get; set; }
    public float Z1 { get; set; }

    public float X2 { get; set; }
    public float Y2 { get; set; }
    public float Z2 { get; set; }

    public Location(int trackid, int serial, float x1, float y1, float z1, float x2, float y2, float z2)
    {
        this.TrackId = trackid;
        this.Serial = serial;

        this.X1 = x1;
        this.Y1 = y1;
        this.Z1 = z1;

        this.X2 = x2;
        this.Y2 = y2;
        this.Z2 = z2;
    }

    public override string ToString()
    {
        return string.Format("Track Id : {6} = {7} [{0}-{1}-{2}] [{3}-{4}-{5}]", this.X1, this.Y1, this.Z1, this.X2, this.Y2, this.Z2, this.TrackId, this.Serial);
    }
}
