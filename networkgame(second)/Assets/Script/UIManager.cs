using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject _networkManagerPraf;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartMaching()
    {
        if (NetworkManager.instance == null)
        {
            Instantiate(_networkManagerPraf);
        }
        SceneManager.LoadScene("Main");
    }

    #if UNITY_EDITOR
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

        if (GUI.Button(new Rect(275, Screen.height / 2, 100, 30), "TCP Connect") && NetworkManager.instance == null)
        {
            Instantiate(_networkManagerPraf);
        }
        if (GUI.Button(new Rect(275, Screen.height / 2 - 30, 100, 30), "TCP DisConnect") && NetworkManager.instance != null)
        {
            //NetworkManager.instance.DisConnect();
            Destroy(NetworkManager.instance.gameObject);
        }
    }
    #endif
}
