using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System;
using System.Linq;
using UnityEngine.Purchasing;
using Newtonsoft.Json;


#if UNITY_EDITOR
using Firebase.Database;
using Firebase.Extensions;

#endif
public static class Extensions
{
    public static string GetString<K, V>(this IDictionary<K, V> dict)
    {
        var items = from kvp in dict
                    select kvp.Key + ":" + kvp.Value;

        return "{" + string.Join(", ", items) + "}";
    }
}

public class DatabaseManager : MonoSingleton<DatabaseManager>, ISubject {
    public int MaxScores = 1000;

#if !UNITY_WEBGL || UNITY_EDITOR
    FirebaseDatabase reference;
#endif
    List<IObserver> _ops = new List<IObserver>();

    public List<object> Leaders { get; private set; }

    protected override void OnCreate()
    {

#if UNITY_WEBGL && !UNITY_EDITOR
        FirebaseWebGL.Scripts.FirebaseBridge.FirebaseDatabase.GetJSON("Leader", gameObject.name, "HandleValueChangedJS", "HandleValueChangedJS");
        //FirebaseWebGL.Scripts.FirebaseBridge.FirebaseDatabase.GetLeader(gameObject.name, "HandleValueChanged");
        //FirebaseWebGL.Scripts.FirebaseBridge.FirebaseDatabase.GetScore(gameObject.name, "HandleValueChanged");
#else
        reference = FirebaseDatabase.DefaultInstance;
        reference.GetReference("Leader").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                if (task.Result.Value == null) {
                    Leaders = new List<object>();
                    NotifyToObserver();
                    return;
                }
                Leaders = task.Result.Value as List<object>;
                //ShowLeader();
                NotifyToObserver();
            }
        });
        reference.GetReference("Leader").ValueChanged += HandleValueChanged;
#endif
    }

    public void ShowState(string state) {
        UIManager.Instance.ShowMessage(state);
    }

    public void AddScoreToLeaders(string userId, int score)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        FirebaseWebGL.Scripts.FirebaseBridge.FirebaseDatabase.AddNewScore(userId, score.ToString());
#else

        reference.GetReference("Leader").RunTransaction(mutableData =>
        {
            List<object> leader = mutableData.Value as List<object>;

            if (leader == null)
            {
                Dictionary<string, object> first = new Dictionary<string, object>();
                first["userId"] = userId;
                first["score"] = score;

                mutableData.Value = new List<object>()
                {
                    first
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
                    long childScore = (long)
                                ((Dictionary<string, object>)child)["score"];
                    if (childScore < minScore)
                    {
                        minScore = childScore;
                        minVal = child;
                    }
                }
                if (minScore > score)
                {
                    // The new score is lower than the existing 5 scores, abort.
                    return TransactionResult.Abort();
                }

                // Remove the lowest score.
                leader.Remove(minVal);
            }
            // Add the new high score.
            Dictionary<string, object> newScoreMap = new Dictionary<string, object>();
            newScoreMap["userId"] = userId;
            newScoreMap["score"] = score;
            leader.Add(newScoreMap);
            mutableData.Value = leader;
            return TransactionResult.Success(mutableData);
        });
#endif
    }

    void HandleValueChangedJS(string data)
    {
        Dictionary<object, Dictionary<string, object>> temp = JsonConvert.DeserializeObject<Dictionary<object, Dictionary<string, object>>>(data);

        Debug.Log(data);
        /*
        foreach (KeyValuePair<object, object> kvp in temp)
        {
            //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
        }

        return;
        */
        Leaders = temp.Values.ToList<object>();
        Debug.Log(Leaders.Count);
        foreach (Dictionary<string, object> element in Leaders)
        {
            Debug.Log(string.Format("score = {0}, userId = {1}", element["score"], element["userId"]));

        }
        NotifyToObserver();
    }

#if !UNITY_WEBGL || UNITY_EDITOR
    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Leaders = args.Snapshot.Value as List<object>;
        //ShowLeader();
        NotifyToObserver();
    }
#endif

    void ShowLeader()
    {

        if (Leaders == null)
        {
            return;

        }
        string result = string.Empty;
        for (int i = 0; i < Leaders.Count; i++)
        {
            result += Extensions.GetString(Leaders[i] as Dictionary<string, object>);
        }
        Debug.Log(result);
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
        foreach (IObserver ops in _ops) {
            ops.Notified();
        }
    }
}