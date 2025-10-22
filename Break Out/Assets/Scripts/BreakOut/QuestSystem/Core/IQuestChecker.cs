public interface IQuestChecker {
    public void Initial();
    public bool CheckAchieve();
    public float GetProgress();
}

public class KillAllEnemyTotalCountQuest : IQuestChecker {
    int _goalCount;

    public KillAllEnemyTotalCountQuest(int goalCount)
    {
        _goalCount = goalCount;
    }

    public void Initial()
    {
        
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
    
    public void Initial()
    {

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
    
    public void Initial()
    {

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