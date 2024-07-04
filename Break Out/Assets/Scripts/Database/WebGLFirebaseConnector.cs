using EHTool;
using EHTool.DBKit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;


public static class WebGLBridge {

    [DllImport("__Internal")]
    public static extern void OnInit(string firebaseConfigValue);
    [DllImport("__Internal")]
    public static extern void PostJSON(string path, string value, string objectName, string callback, string fallback);
    [DllImport("__Internal")]
    public static extern void AddNewScore(string path, string userId, string score);
    [DllImport("__Internal")]
    public static extern void GetJSON(string path, string objectName, string callback, string fallback);

}

public class WebGLFirebaseConnector<T> : MonoBehaviour, IDatabaseConnector<T> where T : IDictionaryable<T> {

    static bool _isConnect = false;

    public int MaxScores = 20;

    ISet<CallbackMethod<IList<T>>> _allCallback;
    protected string _dbName;


    public void Connect(string databaseName)
    {
        _allCallback = new HashSet<CallbackMethod<IList<T>>>();
        _dbName = databaseName;

        if (_isConnect) return;

        WebGLBridge.OnInit(AssetOpener.ReadTextAsset("FirebaseConfig"));
        _isConnect = true;

    }

    public bool IsDatabaseExist()
    {
        return true;
    }

    public virtual void AddRecord(T record)
    {
        if (!_isConnect) return;

        WebGLBridge.PostJSON(_dbName, JsonUtility.ToJson(record.ToDictionary()),
            gameObject.name, "AddRecordCallback", "AddRecordFallback");
    }

    public void UpdateRecordAt(T record, int idx)
    {
        if (!_isConnect) return;

        WebGLBridge.PostJSON(_dbName, JsonUtility.ToJson(record.ToDictionary()),
            gameObject.name, "AddRecordCallback", "AddRecordFallback");
    }

    public void GetRecordAt(CallbackMethod<T> callback, CallbackMethod fallback, int idx)
    {
        if (!_isConnect) return;

        WebGLBridge.GetJSON(_dbName, gameObject.name, "GetRecordAtCallback", "GetRecordAtFallback");
    }

    public void GetAllRecord(CallbackMethod<IList<T>> callback)
    {
        if (!_isConnect) return;

        if (_allCallback.Count > 0) {
            _allCallback.Add(callback);
            return;
        }

        _allCallback.Add(callback);
        WebGLBridge.GetJSON(_dbName, gameObject.name, "GetRecordAtCallback", "GetRecordAtFallback");
    }

    public void GetRecordAtCallback(string value)
    {
        Dictionary<object, T> source = JsonConvert.DeserializeObject<Dictionary<object, T>>(value);
        IList<T> data = source.Values.ToList();

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