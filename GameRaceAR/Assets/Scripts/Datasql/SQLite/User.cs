using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class User : IComparable<User> {

	public string Username { get; set; }
    public int Score { get; set; }
    public int Coin { get; set; }

    public User(string username, int score, int coin)
    {
        this.Username = username;
        this.Score = score;
        this.Coin = coin;
    }

    public override string ToString()
    {
        return string.Format(" Username : {0}, Score : {1}, Coin : {2}",this.Username, this.Score, this.Coin);
    }

    public int CompareTo(User other)
    {
        if (other.Score < this.Score)
            return -1;
        else if (other.Score > this.Score)
            return 1;
        else
            return 0;
    }
}
