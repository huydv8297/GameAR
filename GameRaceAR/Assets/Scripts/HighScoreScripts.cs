using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScripts : MonoBehaviour {

    public GameObject rankText;
    public GameObject nameText;
    public GameObject scoreText;

    public void SetScore(int Rank, string Name, int Score)
    {
        this.rankText.GetComponent<Text>().text = Rank.ToString();
        this.nameText.GetComponent<Text>().text = Name;
        this.scoreText.GetComponent<Text>().text = Score.ToString();
    }

}
