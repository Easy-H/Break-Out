using UnityEngine;

public interface QuestChecker {
    public bool CheckAchieve();
    public float GetProgress();
}

public class KillEnemyQuest : QuestChecker {

    string _enemyName;
    int _goalCount;
    int _startCount;

    public KillEnemyQuest(string enemyName, int goalCount) { 
        _enemyName = enemyName;
        _goalCount = goalCount;
        _startCount = StatisticsManager.Instance.GetKillCount(enemyName);
    }

    public bool CheckAchieve()
    {
        return (StatisticsManager.Instance.GetKillCount(_enemyName) - _startCount) >= _goalCount;
    }

    public float GetProgress()
    {
        return (StatisticsManager.Instance.GetKillCount(_enemyName) - _startCount) / _goalCount; 
    }
}

public class KillAllEnemyQuest : QuestChecker {

    int _goalCount;
    int _startCount;

    public KillAllEnemyQuest(int goalCount)
    {
        _goalCount = goalCount;
        _startCount = StatisticsManager.Instance.GetKillCount();
    }

    public bool CheckAchieve()
    {
        return (StatisticsManager.Instance.GetKillCount() - _startCount) >= _goalCount;
    }

    public float GetProgress()
    {
        return (StatisticsManager.Instance.GetKillCount() - _startCount) / _goalCount;
    }
}

public class KillBallQuest : QuestChecker {
    
    float _goalTime;
    float _spendTime;
    int _originBallCount;

    public KillBallQuest(float goalTime)
    {
        _goalTime = goalTime;
        _spendTime = 0;
        //_originBallCount = 
    }

    public bool CheckAchieve()
    {
        if (_spendTime < _goalTime)
            return false;

        return true;
    }

    public float GetProgress()
    {
        return 1f;
    }
}