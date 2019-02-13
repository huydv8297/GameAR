using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class l_User : MonoBehaviour {

    #region Singeton
    public static l_User l_user;

    public Text m_ConsoleUI;

    private void Awake()
    {
        if(l_user != null)
        {
            Debug.LogWarning("More than one List User !");
            return;
        }
        l_user = this;
    }
    #endregion

    public List<User> Users = new List<User>();

    public void Add(User user)
    {
        Users.Add(user);
        return;
    }

    public void Remove(string username)
    {
        var result = Users.Find(x => x.Username == username);
        if(result != null)
        {
            Users.Remove(result);
        }
        return;
    }

    public void Print()
    {
        m_ConsoleUI.text = "";
        Users.Sort();
        foreach(var user in Users)
        {
            string tmp = user.ToString();
            Debug.Log(tmp);
            m_ConsoleUI.text += System.Environment.NewLine + tmp;
        }
    }

    public void ToConsole()
    {
        Users.Sort();
        foreach(var user in Users)
        {
            string tmp = user.ToString();
            Debug.Log(tmp);
        }
    }

    public bool CheckUserInList(string username)
    {
        User result = Users.Find(x => x.Username == username);
        if (result == null)
        {
            return false;
        }
        return true;
    }

    public User GetCurrentUser()
    {
        if (Users != null)
            return Users[0];
        return null;
    }

    public void UpdateCoin(int price)
    {
        Users[0].Coin -= price;
        Database_Controller.DB.m_User_DB.Update("User", "Coin", Users[0].Coin.ToString(), "Username", Users[0].Username);
    }

}
