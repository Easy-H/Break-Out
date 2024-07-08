using EHTool;
using EHTool.DBKit;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Score : IDictionaryable<Score> {

    public string userId;
    public int score;

    public Score() {
        userId = "";
        score = 0;
    }

    public Score(string userId, int score)
    {
        this.userId = userId;
        this.score = score;
    }

    public IDictionary<string, object> ToDictionary()
    {
        IDictionary<string, object> newScoreMap = new Dictionary<string, object>();

        newScoreMap["userId"] = userId;
        newScoreMap["score"] = score;

        return newScoreMap;
    }

    public void SetValueFromDictionary(IDictionary<string, object> value)
    {
        userId = value["userId"].ToString();
        score = int.Parse(value["score"].ToString());
    }
}

public class DatabaseManager : MonoSingleton<DatabaseManager>, IObservable<IList<Score>> {

    public int MaxScores = 20;

    private readonly ISet<IObserver<IList<Score>>> _observers = new HashSet<IObserver<IList<Score>>>();
    private IDatabaseConnector<Score> _dbConnector;

    public IDisposable Subscribe(IObserver<IList<Score>> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return new Unsubscriber<IList<Score>>(_observers, observer);
    }

    protected override void OnCreate()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _dbConnector = new FirebaseScoreConnector();
#else
        _dbConnector = gameObject.AddComponent<WebGLFirebaseScoreConnector>();
#endif
        _dbConnector.Connect("Leader");

    }

    public void AddScoreToLeaders(int score)
    {
        _dbConnector.AddRecord(new Score(PlayerManager.Instance.PlayerName, score));

    }

    public void GetLeader()
    {
        _dbConnector.GetAllRecord(GetLeaderCallback);

    }

    public void GetLeaderCallback(IList<Score> data)
    {

        foreach (IObserver<IList<Score>> target in _observers)
        {
            target.OnNext(data);
        }

    }
}