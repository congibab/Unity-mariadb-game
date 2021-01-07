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
        if(Input.GetKeyDown(KeyCode.A))
        {
            tcp.Send("A button down");
        }
    }

    private string IPstring = "localhost";
    private string Portstring = "5000";
    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 50), "Top-left");
        GUI.Box(new Rect(Screen.width - 100, 0, 100, 50), "Top-right");
        GUI.Box(new Rect(0, Screen.height - 50, 100, 50), "Bottom-left");
        GUI.Box(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Bottom-right");

        IPstring = GUI.TextArea(new Rect(25, Screen.height / 2, 200, 30), IPstring);
        Portstring = GUI.TextArea(new Rect(225, Screen.height / 2, 50, 30), Portstring);

        bool btn1 = GUI.Button(new Rect(275, Screen.height / 2, 100, 30), "TCP Connect");

        if (btn1 && !tcp.is_Connecting)
        {
            tcp.Connect(IPstring, Portstring);
        }
        if (GUI.Button(new Rect(275, Screen.height / 2 - 30, 100, 30), "TCP DisConnect"))
        {
            tcp.DisConnect();
        }
    }
}
