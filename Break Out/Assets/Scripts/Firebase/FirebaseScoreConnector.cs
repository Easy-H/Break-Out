#if !UNITY_WEBGL || UNITY_EDITOR_WIN
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using System;
using EasyH.Tool.DBKit;

public class FirebaseScoreConnector : IDatabaseConnector<int, Score>
{

    private int MaxScores = 20;

    private DatabaseReference _docRef;

    private Action<IDictionary<int, Score>> _allListener;
    private Action<string> _fallbackListener;

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

    public void Connect(string[] args)
    {
        _docRef = GetReferenceFrom(
            FirebaseDatabase.DefaultInstance.RootReference, args);

        for (int i = 0; i < args.Length; i++)
        {
            _docRef = _docRef.Child(args[i]);
        }

        _allListener = null;
        _fallbackListener = null;
    }

    public void Connect(string authName, string databaseName)
    {
        Connect(new string[2] { databaseName, authName });
    }

    public bool IsDatabaseExist()
    {
        return true;
    }

    public void AddRecord(Score record)
    {
        _docRef.RunTransaction(mutableData =>
        {
            List<object> leader = mutableData.Value as List<object>;

            if (leader == null)
            {
                mutableData.Value = new List<object>()
                {
                    record.ToDictionary()
                };

                return TransactionResult.Success(mutableData);

            }

            if (leader.Count >= MaxScores)
            {
                long minScore = long.MaxValue;
                object minVal = null;

                foreach (var child in leader)
                {
                    if (!(child is Dictionary<string, object>)) continue;

                    long childScore = long.Parse(((Dictionary<string, object>)child)["score"].ToString());

                    if (childScore < minScore)
                    {
                        minScore = childScore;
                        minVal = child;
                    }
                }
                if (minScore > record.score)
                {
                    return TransactionResult.Abort();
                }

                // Remove the lowest score.
                leader.Remove(minVal);
            }

            leader.Add(record.ToDictionary());
            mutableData.Value = leader;

            return TransactionResult.Success(mutableData);

        });
        
    }

    public void UpdateRecordAt(int idx, Score record)
    {
        UpdateRecord(new IDatabaseConnector<int, Score>.UpdateLog[1] { new(idx, record) });
    }

    public void UpdateRecord(IDatabaseConnector<int, Score>.UpdateLog[] updates)
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

    public void GetAllRecord(
        Action<IDictionary<int, Score>> callback,
        Action<string> fallback)
    {
        if (_allListener != null)
        {
            _allListener += callback;
            _fallbackListener += fallback;
            return;
        }

        _docRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {

            if (task.IsFaulted)
            {
            }
            else if (task.IsCompleted)
            {
                IDictionary<int, Score> data
                    = new Dictionary<int, Score>();

                if (task.Result.Value == null)
                {

                }
                else
                {
                    IList<object> temp = task.Result.Value as List<object>;

                    for (int i = 0; i < temp.Count; i++)
                    {
                        Score s = new Score();
                        
                        if (!s.SetValueFromDictionary(temp[i] as Dictionary<string, object>)) continue;

                        data.Add(i, s);
                    }

                }

                _allListener?.Invoke(data);

            }
            _allListener = null;
        });
    }

    public void GetRecordAt(int idx, Action<Score> callback, Action<string> fallback)
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
    
    public void DeleteRecordAt(int idx)
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