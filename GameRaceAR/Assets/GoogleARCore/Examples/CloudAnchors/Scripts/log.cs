using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class log : MonoBehaviour {

    static  Text logText;
    void Start() {
        logText = GetComponent<Text>();
    }

    // Update is called once per frame
   
    public static void SetString(string _text) {
        logText.text = _text;
    }
}
