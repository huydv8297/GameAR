using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour {

    public void Car_Scene()
    {
        SceneManager.LoadScene("Car_Data");
    }

    public void User_Scene()
    {
        SceneManager.LoadScene("User_Data");
    }

    public void Item_Scene()
    {
        SceneManager.LoadScene("Item_Data");
    }

    public void Shop_Scene()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Build_Track()
    {
        SceneManager.LoadScene("Build_Track");
    }
}
