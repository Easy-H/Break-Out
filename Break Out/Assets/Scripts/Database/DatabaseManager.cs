using EHTool;
using EHTool.DBKit;
using System;
using System.Collections.Generic;

public class Score : IDictionaryable<Score> {
    public string userId;
    public int score;

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

    public static Score FromDictionary(IDictionary<string, object> d)
    {
        return new Score(d["userId"].ToString(), int.Parse(d["score"].ToString()));
    }
}

public class DatabaseManager : MonoSingleton<DatabaseManager>, IObservable<IList<Score>> {

    public int MaxScores = 20;

    private readonly ISet<IObserver<IList<Score>>> _observers = new HashSet<IObserver<IList<Score>>>();
    private IDatabaseConnector<Score> _dbReader;

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
        _dbReader = new FirebaseScoreConnector();
#else
        _dbReader = gameObject.AddComponent<WebGLFirebaseScoreConnector>();
#endif
        _dbReader.Connect("Leader");

    }

    public void AddScoreToLeaders(int score)
    {
        _dbReader.AddRecord(new Score(PlayerManager.Instance.PlayerName, score));

    }

    public void GetLeader()
    {
        _dbReader.GetAllRecord(GetLeaderCallback);

    }

    public void GetLeaderCallback(IList<Score> data)
    {

        foreach (IObserver<IList<Score>> target in _observers)
        {
            target.OnNext(data);
        }

    }
}