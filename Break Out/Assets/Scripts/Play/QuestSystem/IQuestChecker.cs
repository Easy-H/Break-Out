public interface IQuestChecker {
    public bool CheckAchieve();
    public float GetProgress();
}

public class KillEnemyQuest : IQuestChecker {

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

    public override string ToString() { 
        return string.Format("{0}/{1}", StatisticsManager.Instance.GetKillCount(_enemyName) - _startCount, _goalCount);
    }
}

public class KillAllEnemyQuest : IQuestChecker {

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
    public override string ToString()
    {
        return string.Format("{0}/{1}", StatisticsManager.Instance.GetKillCount() - _startCount, _goalCount);
    }
}

public class KillAllEnemyTotalCountQuest : IQuestChecker {
    int _goalCount;

    public KillAllEnemyTotalCountQuest(int goalCount)
    {
        _goalCount = goalCount;
    }

    public bool CheckAchieve()
    {
        return StatisticsManager.Instance.GetKillCount() >= _goalCount;
    }

    public float GetProgress()
    {
        return StatisticsManager.Instance.GetKillCount() / _goalCount;
    }
    public override string ToString()
    {
        return string.Format("{0}/{1}", StatisticsManager.Instance.GetKillCount(), _goalCount);
    }

}

public class BestScoreQuest : IQuestChecker {

    int _goal;
    public BestScoreQuest(int goal) {
        _goal = goal;
    }

    public bool CheckAchieve()
    {
        return StatisticsManager.Instance.GetBestScore() >= _goal;
    }

    float IQuestChecker.GetProgress()
    {
        return (StatisticsManager.Instance.GetBestScore()) / _goal;

    }

    public override string ToString()
    {
        return string.Format("{0}/{1}", StatisticsManager.Instance.GetBestScore(), _goal);
    }
}

public class KillBallQuest : IQuestChecker {
    
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