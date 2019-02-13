using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class User_Manager : MonoBehaviour {

    private string m_databaseName = "UserDataBase.db";
    private string m_tableName = "User";

    public InputField _IP_username;
    public InputField _IP_score;
    public InputField _IP_coin;

    public Text m_managerText;

    private Database_Connection m_User_DB;

    private void Start()
    {
        m_User_DB = new Database_Connection(m_databaseName);
        m_User_DB.selectTable(m_databaseName, m_tableName);
    }

    public void f_PrintUser()
    {
        Debug.Log("--------print user !--------");
        l_User.l_user.Print();
        Debug.Log("----------------------------");
    }

    public void f_InsertUser()
    {
        if (_IP_username.text == "" || _IP_coin.text == "" || _IP_score.text == "")
        {
            m_managerText.text = "Please check Inputfeild !";
            return;
        }
        else
        {
            string userIP = _IP_username.text;

            if(l_User.l_user.CheckUserInList(userIP))
            {
                m_managerText.text = userIP + " is avairble !";
                return;
            }
            Debug.Log("--------Insert User--------");
            User user = new User(userIP, int.Parse(_IP_score.text), int.Parse(_IP_coin.text));
            l_User.l_user.Add(user);
            m_User_DB.InsertUser(user);
            m_managerText.text = userIP + " inserted !";
            Debug.Log("----------------------------");

            return;
        }
    }

    public void f_RemoveUser()
    {
        string userIP = _IP_username.text;

        if (userIP == "")
        {
            m_managerText.text = "Please check User Id Inputfeild !";
            return;
        }
        else
        {
            if (l_User.l_user.CheckUserInList(userIP) == false)
            {
                m_managerText.text = userIP + " is null !";
                return;
            }
            Debug.Log("--------remove user !--------");
            l_User.l_user.Remove(userIP);
            m_User_DB.Remove(m_tableName, "Username", userIP);
            m_managerText.text = userIP + " removed !";
            Debug.Log("----------------------------");

            return;
        }
    }
    public void change_Scene()
    {
        SceneManager.LoadScene("Menu");
    }
}
