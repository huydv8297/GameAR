using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Track {

    public int TrackId { get; set; }

    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Track(int trackid, float x, float y, float z)
    {
        this.TrackId = trackid;

        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public override string ToString()
    {
        return string.Format("Track Id : {3} = Check Point : [{0}-{1}-{2}]", this.X, this.Y, this.Z, this.TrackId);
    }
}
