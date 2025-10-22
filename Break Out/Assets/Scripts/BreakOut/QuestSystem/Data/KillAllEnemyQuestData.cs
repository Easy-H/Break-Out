using UnityEngine;

[CreateAssetMenu(fileName = "Quest_",
    menuName = "Scriptable Object/QuestData/KillAllEnemy")]
public class KillAllEnemyQuestData : QuestDataBase
{
    [SerializeField] private int _goalKillCount;

    public override IQuestChecker GetQuestChecker()
    {
        return new KillAllEnemyQuestChecker(_goalKillCount);
    }
    
}