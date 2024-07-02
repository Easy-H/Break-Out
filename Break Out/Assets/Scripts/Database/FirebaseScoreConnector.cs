#if !UNITY_WEBGL || UNITY_EDITOR
using EHTool.DBKit;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;

public class FirebaseScoreConnector : IDatabaseConnector<Score> {

    public int MaxScores = 20;

    DatabaseReference reference;

    ISet<CallbackMethod<IList<Score>>> _allCallback;
    IDictionary<CallbackMethod<Score>, ISet<int>> _recordCallback;

    IDictionary<CallbackMethod<Score>, CallbackMethod> _recordFallback;


    public void Connect(string databaseName)
    {
        reference = FirebaseDatabase.DefaultInstance.GetReference(databaseName);

        _allCallback = new HashSet<CallbackMethod<IList<Score>>>();
        _recordCallback = new Dictionary<CallbackMethod<Score>, ISet<int>>();
        _recordFallback = new Dictionary<CallbackMethod<Score>, CallbackMethod>();
    }

    public bool IsDatabaseExist()
    {
        return true;
    }

    public void AddRecord(Score record)
    {
        reference.RunTransaction(mutableData =>
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

    public void UpdateRecordAt(Score Record, int idx)
    {
        IDictionary<string, object> updateValue = new Dictionary<string, object>
        {
            { idx.ToString(), Record }
        };

        reference.UpdateChildrenAsync(updateValue);
    }

    public void GetAllRecord(CallbackMethod<IList<Score>> callback)
    {
        _allCallback.Add(callback);

        reference.GetValueAsync().ContinueWithOnMainThread(task => {

            if (task.IsFaulted)
            {
            }
            else if (task.IsCompleted)
            {
                IList<Score> data = new List<Score>();

                if (task.Result.Value == null)
                {
                }
                else
                {
                    IList<object> temp = task.Result.Value as List<object>;

                    for (int i = 0; i < temp.Count; i++)
                    {
                        data.Add(Score.FromDictionary(temp[i] as Dictionary<string, object>));
                    }

                }

                foreach (CallbackMethod<IList<Score>> cb in _allCallback)
                {
                    cb(data);
                }

            }
            _allCallback = new HashSet<CallbackMethod<IList<Score>>>();
        });
    }

    public void GetRecordAt(CallbackMethod<Score> callback, CallbackMethod fallback, int idx)
    {
        if (!_recordCallback.ContainsKey(callback))
        {
            _recordCallback.Add(callback, new HashSet<int>());
            _recordFallback.Add(callback, fallback);
        }

        _recordCallback[callback].Add(idx);
        _recordFallback[callback] = fallback;

        if (_allCallback.Count > 0)
        {
            _allCallback.Add(Callback);
            return;
        }
    }

    public void Callback(IList<Score> data)
    {
        foreach (KeyValuePair<CallbackMethod<Score>, ISet<int>> callback in _recordCallback)
        {
            foreach (int idx in callback.Value)
            {
                if (data.Count > idx)
                    callback.Key(data[idx]);
                else
                    _recordFallback[callback.Key]();

            }
        }

        _recordCallback = new Dictionary<CallbackMethod<Score>, ISet<int>>();
        _recordFallback = new Dictionary<CallbackMethod<Score>, CallbackMethod>();
    }
}

#endif