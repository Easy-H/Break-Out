using System;
using System.Collections.Generic;
using EasyH;
using EasyH.Unity;
using EasyH.Tool.DBKit;
using System.Linq;

public class FirebaseWebGLScoreConnector :
    FirebaseWebGLConnector<string, Score> { }

public class DatabaseManager : MonoSingleton<DatabaseManager>,
    IObservable<IList<Score>>
{

    private int _scoreLeaderCnt = 20;

    private readonly ISet<IObserver<IList<Score>>> _observers
        = new HashSet<IObserver<IList<Score>>>();
    private IDatabaseConnector<string, Score> _dbConnector;

    public IDictionary<string, Score> _dic;

    public IDisposable Subscribe(IObserver<IList<Score>> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
            if (_dic != null)
            {
                observer.OnNext(GetScoreLeader());
            }
        }

        return new Unsubscriber<IList<Score>>(_observers, observer);
    }

    protected override void OnCreate()
    {
        _dic = new Dictionary<string, Score>();
        
#if !UNITY_WEBGL || UNITY_EDITOR
        _dbConnector = new FirebaseConnector<string, Score>();
#else
        _dbConnector
            = gameObject.AddComponent<FirebaseWebGLScoreConnector>();
#endif
        _dbConnector.Connect(new string[1] { "Leader" });
        GetLeader();
    }

    public void AddScoreToLeaders(int score)
    {
        _dbConnector.AddRecord(new Score(
            PlayerManager.Instance.PlayerName, score));

        IList<Score> scoreList = GetScoreLeader();

        if (scoreList == null) return;

        if (scoreList.Count < _scoreLeaderCnt) return;

        int targetScore = scoreList[_scoreLeaderCnt].score;

        IList<IDatabaseConnector<string, Score>.UpdateLog> updates
            = new List<IDatabaseConnector<string, Score>.UpdateLog>();

        foreach (var s in _dic)
        {
            if (s.Value.score >= targetScore) continue;

            updates.Add(
                new IDatabaseConnector<string, Score>.
                    UpdateLog(s.Key, null));
        }

        _dbConnector.UpdateRecord(updates.ToArray());

    }

    public void GetLeader()
    {
        _dbConnector.GetAllRecord(GetLeaderCallback, (msg) => { });

    }

    public void GetLeaderCallback(IDictionary<string, Score> data)
    {
        _dic = data;
        Notify();
        
    }

    private IList<Score> GetScoreLeader()
    {
        if (_dic == null) return null;

        List<Score> ret = _dic.Values.ToList();
        ret.Sort((a, b) =>
        {
            return -a.score.CompareTo(b.score);
        });

        return ret;
        
    }

    private void Notify()
    {
        if (_dic == null) return;

        IList<Score> ret = GetScoreLeader();
        
        foreach (IObserver<IList<Score>> target in _observers)
        {
            target.OnNext(ret);
        }
    }

}