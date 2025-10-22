public class KillAllEnemyQuestChecker : IQuestChecker
{

    private int _goalCount;
    private int _startCount;

    public KillAllEnemyQuestChecker(int goalCount)
    {
        _goalCount = goalCount;
        _startCount = StatisticsManager.Instance.GetKillCount();
    }

    public void Initial()
    {
        _startCount = StatisticsManager.Instance.GetKillCount();
    }

    public bool CheckAchieve()
    {
        return (StatisticsManager.Instance.GetKillCount()
            - _startCount) >= _goalCount;
    }

    public float GetProgress()
    {
        return (StatisticsManager.Instance.GetKillCount()
            - _startCount) / _goalCount;
    }

    public override string ToString()
    {
        return string.Format("{0}/{1}",
            StatisticsManager.Instance.GetKillCount()
                - _startCount, _goalCount);
    }
    
}