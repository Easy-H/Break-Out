
public class KillEnemyQuestChecker : IQuestChecker {

    private string _enemyName;
    private int _goalCount;
    private int _startCount;

    public KillEnemyQuestChecker(string enemyName, int goalCount) { 
        _enemyName = enemyName;
        _goalCount = goalCount;
        _startCount = 0;
    }

    public void Initial()
    {
        _startCount = StatisticsManager.Instance.
            GetKillCount(_enemyName);
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
