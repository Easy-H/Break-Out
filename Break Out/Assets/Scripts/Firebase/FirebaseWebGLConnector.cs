using EasyH.Tool.DBKit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

static class FirebaseWebGLBridge
{

    [DllImport("__Internal")]
    public static extern void FirebaseAddRecord(string pathJson, string recordJson, string idx);

    [DllImport("__Internal")]
    public static extern void FirebaseUpdateRecord(string pathJson, string updateJson);

    [DllImport("__Internal")]
    public static extern void FirebaseGetAllRecord(string pathJson, string objectName, string callback, string fallback);

    [DllImport("__Internal")]
    public static extern void FirebaseDeleteRecordAt(string pathJson, string idx);
    
    [DllImport("__Internal")]
    public static extern string FirebaseDefaultKeyGet(string pathJson);
}

public class FirebaseWebGLConnector<K, T> : MonoBehaviour, IDatabaseConnector<K, T> where T : IDictionaryable<T>
{

    static private bool _isConnect = false;

    private Action<IDictionary<K, T>> _allListener;
    private Action<string> _fallbackListener;
    private Func<K> _addKeyGetter;

    private string _pathJson;

    private bool _dbExist;

    private string GetAddKey()
    {
        if (_addKeyGetter == null)
        {
            return FirebaseWebGLBridge.FirebaseDefaultKeyGet(_pathJson);
        }
        return _addKeyGetter().ToString();
    }

    public bool IsDatabaseExist()
    {
        return _dbExist;
    }

    public void Connect(string[] args)
    {
        _pathJson = JsonConvert.SerializeObject(args);

        _allListener = null;
        _fallbackListener = null;

        if (_isConnect) return;

        FirebaseManager.Instance.SetConfig();
        _isConnect = true;

    }

    public void Connect(string authName, string databaseName)
    {
        Connect(new string[2] { databaseName, authName });
    }

    public void AddRecord(T record)
    {
        if (!_isConnect) return;
        string key = GetAddKey();
        
        FirebaseWebGLBridge.FirebaseAddRecord(_pathJson,
            JsonConvert.SerializeObject(record.ToDictionary()),
            key);
    }

    public void UpdateRecordAt(K idx, T record)
    {
        UpdateRecord(new IDatabaseConnector<K, T>.UpdateLog[1]
            { new (idx, record)});
    }

    public void UpdateRecord(IDatabaseConnector<K, T>.UpdateLog[] updates)
    {
        if (!_isConnect) return;

        Dictionary<string, object> up = new Dictionary<string, object>();

        foreach (var r in updates)
        {
            if (r.Record == null)
            {
                up.Add(r.Idx.ToString(), null);
                continue;
            }
            up.Add(r.Idx.ToString(), r.Record.ToDictionary());
        }

        FirebaseWebGLBridge.FirebaseUpdateRecord(
            _pathJson,
            JsonConvert.SerializeObject(up));
        _dbExist = true;
    }

    public void DeleteRecordAt(K idx)
    {
        FirebaseWebGLBridge.FirebaseDeleteRecordAt(_pathJson, JsonConvert.SerializeObject(idx));
    }

    public void GetAllRecord(Action<IDictionary<K, T>> callback, Action<string> fallback)
    {
        if (!_isConnect) return;

        if (_allListener != null)
        {
            _allListener += callback;
            _fallbackListener += fallback;
            return;
        }

        _allListener += callback;
        _fallbackListener += fallback;

        FirebaseWebGLBridge.FirebaseGetAllRecord(
            _pathJson, gameObject.name,
            "FBGetAllRecordCallback", "FBGetAllRecordFallback");
    }

    public void FBGetAllRecordCallback(string value)
    {

        _dbExist = true;

        Dictionary<string, object> snapshot =
            JsonConvert.DeserializeObject<Dictionary<string, object>>(value);

        IDictionary<K, T> data = new Dictionary<K, T>();

        foreach (var child in snapshot)
        {
            K key = (K)Convert.ChangeType(child.Key, typeof(K));
            T temp = Activator.CreateInstance<T>();

            if (!temp.SetValueFromDictionary(
                JsonConvert.DeserializeObject
                    <Dictionary<string, object>>(
                        child.Value.ToString()))) continue;

            data.Add(key, temp);
        }

        _allListener?.Invoke(data);

        _allListener = null;
        _fallbackListener = null;
    }

    public void FBGetAllRecordFallback()
    {
        _dbExist = false;

        _fallbackListener?.Invoke("Network Error");

        _allListener = null;
        _fallbackListener = null;

    }

    struct IdxAndFallback
    {
        public int idx;
        public Action<string> fallback;

        public IdxAndFallback(int idx, Action<string> fallback)
        {
            this.idx = idx;
            this.fallback = fallback;
        }
    }

    public void GetRecordAt(K idx, Action<T> callback, Action<string> fallback)
    {
        if (!_isConnect) return;

        Action<IDictionary<K, T>> thisCallback = (IDictionary<K, T> data) =>
        {
            if (data.ContainsKey(idx))
            {
                callback?.Invoke(data[idx]);
                return;
            }

            fallback?.Invoke("No Idx");
        };

        GetAllRecord(thisCallback, fallback);
    }

}