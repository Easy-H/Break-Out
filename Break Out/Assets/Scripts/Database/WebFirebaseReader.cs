using EHTool;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class FirebaseError {
    public string code;
    public string message;
    public string details;
}

public class WebFirebaseReader : MonoBehaviour, IDatabaseReader {

    public List<object> Leaders { get; private set; }

    IList<IObserver> _ops = new List<IObserver>();

    [DllImport("__Internal")]
    public static extern void OnInit(string firebaseConfigValue);
    [DllImport("__Internal")]
    public static extern void PostJSON(string path, string value, string objectName, string callback, string fallback);
    [DllImport("__Internal")]
    public static extern void AddNewScore(string userId, string score);
    [DllImport("__Internal")]
    public static extern void GetJSON(string path, string objectName, string callback, string fallback);

    public void OnCreate()
    {
        OnInit(AssetOpener.ReadTextAsset("FirebaseConfig"));
        GetJSON("Leader", gameObject.name, "_HandleValueChanged", "_HandleValueChanged");

    }

    void _HandleValueChanged(string data)
    {
        Dictionary<object, Dictionary<string, object>> temp = JsonConvert.DeserializeObject<Dictionary<object, Dictionary<string, object>>>(data);

        Debug.Log(data);

        if (temp != null)
        {
            Leaders = temp.Values.ToList<object>();
        }
        else
        {
            Leaders = new List<object>();
        }

        NotifyToObserver();
    }

    public void AddScoreToLeaders(string userId, int score)
    {
        AddNewScore(userId, score.ToString());
    }

    public void AddObserver(IObserver ops)
    {
        _ops.Add(ops);
    }

    public void RemoveObserver(IObserver ops)
    {
        _ops.Remove(ops);
    }

    public void NotifyToObserver()
    {
        foreach (IObserver ops in _ops)
        {
            ops.Notified();
        }
    }
}