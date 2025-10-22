using System.Collections.Generic;
using EasyH;

public class StatisticsManager : Singleton<StatisticsManager> {

    private Dictionary<string, int> _killData;
    private int _ballOutCount;
    private int _ballCreateCount;
    private int _bestScore;

    protected override void OnCreate()
    {
        base.OnCreate();
        _killData = new Dictionary<string, int>();
    }

    public void AddKillData(string killedEnemyName) {
        if (!_killData.ContainsKey(killedEnemyName)) {
            _killData.Add(killedEnemyName, 1);
            return;
        }

        _killData[killedEnemyName] = _killData[killedEnemyName] + 1;
    }

    public void NewScore(int score)
    {
        DatabaseManager.Instance.AddScoreToLeaders(score);
        if (_bestScore >= score) return;
        _bestScore = score;
    }

    public void BallOut() {
        _ballOutCount++;
    }

    public void BallCreate() {
        _ballCreateCount++;
    }

    public int GetKillCount() {
        int retval = 0;
        foreach (int count in _killData.Values) {
            retval += count;
        }
        return retval;
    }

    public int GetKillCount(string enemyName) {
        if (!_killData.ContainsKey(enemyName)) return 0;

        return _killData[enemyName];
    }

    public int GetBallCreateCount() {
        return _ballCreateCount;
    }

    public int GetBallOutCount()
    {
        return _ballOutCount;
    }

    public int GetBestScore() {
        return _bestScore;
    }

    public int GetBallGap() {
        return _ballCreateCount - _ballOutCount;
    }

}
