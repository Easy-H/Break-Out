using UnityEngine;

[CreateAssetMenu(fileName = "Quest_",
    menuName = "Scriptable Object/QuestData/KillEnemy")]
public class KillEnemyQuestData : QuestDataBase
{
    [SerializeField] private string _targetEnemyCode;
    [SerializeField] private int _goalKillCount;

    public override IQuestChecker GetQuestChecker()
    {
        return new KillEnemyQuestChecker(_targetEnemyCode, _goalKillCount);
    }

}