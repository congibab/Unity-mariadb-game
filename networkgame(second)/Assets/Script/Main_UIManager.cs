using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_UIManager : MonoBehaviour
{
    [SerializeField]
    Text otherID;
    [SerializeField]
    Text ownID;

    static public Main_UIManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(clientStatus.OwnID != "" && clientStatus.AnotherID != "")
        {
            json_data user = new json_data();
            user.type = "GameStart";
            NetworkManager.instance.Send(json_data.CreateToJSON(user));
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetUserData();
    }

    public void SetUserData()
    {
        ownID.text = clientStatus.OwnID;
        otherID.text = clientStatus.AnotherID;
    }
}
