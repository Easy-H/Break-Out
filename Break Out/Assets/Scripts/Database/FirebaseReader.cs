#if !UNITY_WEBGL || UNITY_EDITOR
using EHTool;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions {
    public static string GetString<K, V>(this IDictionary<K, V> dict)
    {
        var items = from kvp in dict
                    select kvp.Key + ":" + kvp.Value;

        return "{" + string.Join(", ", items) + "}";
    }
}

public class FirebaseReader : IDatabaseReader {
    public int MaxScores = 20;

    FirebaseDatabase reference;

    IList<IObserver> _ops = new List<IObserver>();

    public List<object> Leaders { get; private set; }

    public void OnCreate()
    {
        reference = FirebaseDatabase.DefaultInstance;
        reference.GetReference("Leader").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                if (task.Result.Value == null)
                {
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

    }
    
    public void AddScoreToLeaders(string userId, int score)
    {

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
                    long childScore = long.Parse(((Dictionary<string, object>)child)["score"].ToString());
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
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Leaders = args.Snapshot.Value as List<object>;
        NotifyToObserver();
    }

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
        foreach (IObserver ops in _ops)
        {
            ops.Notified();
        }
    }
}

#endif