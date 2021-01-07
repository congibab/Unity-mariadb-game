using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TCP_Client tcp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string IPstring;
    private string Portstring;
    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 50), "Top-left");
        GUI.Box(new Rect(Screen.width - 100, 0, 100, 50), "Top-right");
        GUI.Box(new Rect(0, Screen.height - 50, 100, 50), "Bottom-left");
        GUI.Box(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Bottom-right");

        IPstring = GUI.TextArea(new Rect(25, Screen.height / 2, 200, 30), IPstring);
        Portstring = GUI.TextArea(new Rect(225, Screen.height / 2, 50, 30), Portstring);
        if(GUI.Button (new Rect(275, Screen.height / 2, 100, 30), "TCP Connect"))
        {
            tcp.Connect(IPstring, Portstring);
        }
    }
}
