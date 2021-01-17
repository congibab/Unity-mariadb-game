using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class json_data
{
    public string type;
    public string id;

    public static json_data CreateFromJSON(string data)
    {
        return JsonUtility.FromJson<json_data>(data);
    }

    public static string CreateToJSON(json_data data)
    {
        return JsonUtility.ToJson(data);
    }
}