#if UNITY_WEBGL && !UNITY_EDITOR

using EHTool;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WebFirebaseReader : IDatabaseReader {

    public List<object> Leaders { get; private set; }
    IList<IObserver> _ops = new List<IObserver>();

    
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

    public void OnCreate()
    {

        FirebaseWebGL.Scripts.FirebaseBridge.FirebaseDatabase.GetJSON("Leader", gameObject.name, "HandleValueChangedJS", "HandleValueChangedJS");
        
    }

    void HandleValueChangedJS(string data)
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

}

#endif