using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserJSON
{
    public string type;
    public string id;

    public static UserJSON CreateFromJSON(string data)
    {
        return JsonUtility.FromJson<UserJSON>(data);
    }

    public static string CreateToJSON(UserJSON data)
    {
        return JsonUtility.ToJson(data);
    }
}