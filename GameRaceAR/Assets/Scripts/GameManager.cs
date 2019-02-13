using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    [SerializeField]
    private float Cash;
    public int CurrentCarID = 0;


    public Text cashText;
	// Use this for initialization
	void Start () {
        gameManager = this;
        UpdateUI();
	}
	
	public void AddCash(float amount)
    {
        Cash += amount;
        UpdateUI();
    }
    public void ReduceCash(float amount)
    {
        Cash -= amount;
        UpdateUI();
    }

    public bool RequestCash(float amount)
    {
        if(amount <= Cash)
        {
            return true;
        }
        return false;
    }

    public float GetCashInfo()
    {
        return Cash;
    }

    public void SetMoneyInfo(float amount)
    {
        Cash = amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        cashText.text = "$ " + Cash.ToString();
    }
}
