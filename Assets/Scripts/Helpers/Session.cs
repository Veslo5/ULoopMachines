using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : Singleton<Session>
{
    private Dictionary<string, string> sessionData = new Dictionary<string, string>();

    public void SetString(string key, string data) => sessionData.Add(key, data);

    public string GetString(string key) => sessionData[key];

    public bool HasKey(string key) => sessionData.ContainsKey(key);

    public void DeleteKey(string key) => sessionData.Remove(key);

}
