using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Quest_", menuName = "Scriptable Object/QuestData")]
public class QuestData : ScriptableObject
{
    public enum QuestType { 
        killEnemy, killAllEnemy, 
    }

    public QuestType Type;
    public string Name;

    public string StringValue;
    public int IntValue;
    public float FloatValue;

    public QuestChecker GetQuestChecker() {
        switch (Type) { 
            case QuestType.killEnemy:
                return new KillEnemyQuest(StringValue, IntValue);
            case QuestType.killAllEnemy:
                return new KillAllEnemyQuest(IntValue);
            default:
                return null;
        }
    }

}
