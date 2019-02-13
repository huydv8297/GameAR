using System;
using UnityEngine;

public class Round : MonoBehaviour {

    public int RoundId { get; set; }
    public int[] Tracks { get; set; }

    public Round(int roundid, string tracks)
    {
        this.RoundId = roundid;
        this.Tracks = SplitString(tracks);
    }

    public override string ToString()
    {
        return string.Format("[Round Id : {0}, Tracks : {1}]", this.RoundId, itemToString(this.Tracks));
    }

    private int[] SplitString(string str)
    {
        return Array.ConvertAll(str.Split('.'), int.Parse);
    }

    private string itemToString(int[] integer)
    {
        string result = "";
        for (int i = 0; i < integer.Length; i++)
        {
            result += integer[i] + " ";
        }
        return result;
    }
}
