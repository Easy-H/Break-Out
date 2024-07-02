using EHTool;
using EHTool.DBKit;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public static class WebGLBridge {

    [DllImport("__Internal")]
    public static extern void OnInit(string path, string firebaseConfigValue);
    [DllImport("__Internal")]
    public static extern void PostJSON(string value, string objectName, string callback, string fallback);
    [DllImport("__Internal")]
    public static extern void AddNewScore(string userId, string score);
    [DllImport("__Internal")]
    public static extern void GetJSON(string objectName, string callback, string fallback);

}

public class WebGLFirebaseConnector<T> : MonoBehaviour, IDatabaseConnector<T> where T : IDictionaryable<T> {

    public int MaxScores = 20;

    bool _isConnect = false;

    ISet<CallbackMethod<IList<T>>> _allCallback;

    public void Connect(string databaseName)
    {
        WebGLBridge.OnInit(databaseName, AssetOpener.ReadTextAsset("FirebaseConfig"));

        _isConnect = true;
        _allCallback = new HashSet<CallbackMethod<IList<T>>>();

    }

    public bool IsDatabaseExist()
    {
        return true;
    }

    public virtual void AddRecord(T record)
    {
        if (!_isConnect) return;
        WebGLBridge.PostJSON(JsonUtility.ToJson(record.ToDictionary()),
            gameObject.name, "AddRecordCallback", "AddRecordFallback");
    }

    public void UpdateRecordAt(T record, int idx)
    {
        if (!_isConnect) return;
        WebGLBridge.PostJSON(JsonUtility.ToJson(record.ToDictionary()),
            gameObject.name, "AddRecordCallback", "AddRecordFallback");
    }

    public void GetRecordAt(CallbackMethod<T> callback, CallbackMethod fallback, int idx)
    {
        if (!_isConnect) return;
        WebGLBridge.GetJSON(gameObject.name, "GetRecordAtCallback", "GetRecordAtFallback");
    }

    public void GetAllRecord(CallbackMethod<IList<T>> callback)
    {
        //        if (!_isConnect) return;
        if (_allCallback.Count > 0) {
            _allCallback.Add(callback);
            return;
        }

        _allCallback.Add(callback);
        WebGLBridge.GetJSON(gameObject.name, "GetRecordAtCallback", "GetRecordAtFallback");
    }

    public void GetRecordAtCallback(string value)
    {
        IList<T> data = new List<T>();
        IList<object> temp = JsonUtility.FromJson<T>(value) as List<object>;

        for (int i = 0; i < temp.Count; i++)
        {
            data.Add(IDictionaryable<T>.FromDictionary(temp[i] as Dictionary<string, object>));
        }

        foreach (CallbackMethod<IList<T>> cb in _allCallback)
        {
            cb(data);
        }

        _allCallback = new HashSet<CallbackMethod<IList<T>>>();

    }

    public void GetRecordAtFallback()
    {

    }
}