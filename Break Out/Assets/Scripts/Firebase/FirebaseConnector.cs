#if !UNITY_WEBGL || UNITY_EDITOR

using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using System.Collections.Generic;
using System;
using EasyH.Tool.DBKit;

public class FirebaseConnector<K, T> : IDatabaseConnector<K, T> where T : IDictionaryable<T>
{

    private DatabaseReference _docRef;

    private Action<IDictionary<K, T>> _allListener;
    private Action<string> _fallbackListener;
    private Func<K> _addKeyGetter;

    private string GetAddKey()
    {
        if (_addKeyGetter == null)
        {
            return _docRef.Push().Key;
        }
        return _addKeyGetter().ToString();
    }

    private bool _databaseExist = false;

    private DatabaseReference GetReferenceFrom(DatabaseReference def, string[] path)
    {
        if (path == null) return def;

        for (int i = 0; i < path.Length; i++)
        {
            def = def.Child(path[i]);
        }

        return def;

    }

    public bool IsDatabaseExist()
    {
        return _databaseExist;
    }

    public FirebaseConnector()
    {

    }

    public FirebaseConnector(Func<K> getter)
    {
        _addKeyGetter = getter;
    }

    public void Connect(string[] args)
    {
        _docRef = GetReferenceFrom(
            FirebaseDatabase.DefaultInstance.RootReference, args);

        _allListener = null;
        _fallbackListener = null;
    }

    public void Connect(string authName, string databaseName)
    {
        Connect(new string[2] { databaseName, authName });

    }

    public void AddRecord(T record)
    {
        Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { GetAddKey() , record.ToDictionary() }
        };

        _docRef.UpdateChildrenAsync(updates).ContinueWithOnMainThread(task =>
        {
            
        });
    }

    public void UpdateRecordAt(K idx, T record)
    {
        UpdateRecord(new IDatabaseConnector<K, T>.UpdateLog[1] { new(idx, record) });
    }

    public void UpdateRecord(IDatabaseConnector<K, T>.UpdateLog[] updates)
    {
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

        _docRef.UpdateChildrenAsync(up);

        if (!_databaseExist)
        {
            _databaseExist = true;
        }

    }

    public void GetAllRecord(Action<IDictionary<K, T>> callback, Action<string> fallback)
    {
        if (_allListener != null)
        {
            _allListener += callback;
            _fallbackListener += fallback;
            return;
        }

        _allListener = callback;
        _fallbackListener = fallback;

        _docRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            DataSnapshot snapshot = task.Result;

            _databaseExist = snapshot.Exists;

            if (snapshot.Exists)
            {
                IDictionary<K, T> data = new Dictionary<K, T>();

                foreach (DataSnapshot snap in snapshot.Children)
                {
                    K key = (K)Convert.
                        ChangeType(snap.Key, typeof(K));
                    T value = Activator.CreateInstance<T>();

                    if (!(snap.Value is Dictionary<string, object>
                        childDataDictionary)) continue;
                    
                    if (!value.SetValueFromDictionary(
                        childDataDictionary)) continue;
                    
                    data.Add(key, value);

                }

                _allListener?.Invoke(data);
            }
            else
            {
                _fallbackListener?.Invoke(string.Format("Document {0} does not exist!", snapshot.Key.ToString()));
                Debug.LogFormat("Document {0} does not exist!", snapshot.Key.ToString());

            }

            _allListener = null;
            _fallbackListener = null;

        });
    }

    // GetRecordAll에서 모든 레코드 받아오면 거기서 원하는걸 찾아오는 방식임
    // 비효율적인 방식이지만 이 게임에서 이걸 사용하는 건 하나밖에 없어서(MetaData) 일단은 이렇게 둠
    public void GetRecordAt(K idx, Action<T> callback, Action<string> fallback)
    {
        GetAllRecord((data) =>
        {
            if (data.ContainsKey(idx))
            {
                callback?.Invoke(data[idx]);
                return;
            }

            fallback?.Invoke("No Idx");
        }, fallback);

    }

    public void DeleteRecordAt(K idx)
    {

        Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { idx.ToString(), null }
        };

        _docRef.UpdateChildrenAsync(updates);

        if (!_databaseExist)
        {
            _databaseExist = true;
        }
    }

}
#endif