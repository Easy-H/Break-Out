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

    public string Value;

    public IQuestChecker GetQuestChecker() {
        switch (Type) { 
            case QuestType.killEnemy:
                string[] s = Value.Split(',');
                return new KillEnemyQuest(s[0], int.Parse(s[1]));
            case QuestType.killAllEnemy:
                return new KillAllEnemyQuest(int.Parse(Value));
            default:
                return null;
        }
    }

    public static IQuestChecker GetQuestChecker(string value) {

        string[] s = value.Split(',');

        switch (s[0])
        {
            case "killEnemy":
                return new KillEnemyQuest(s[1], int.Parse(s[2]));
            case "killAllEnemy":
                return new KillAllEnemyQuest(int.Parse(s[1]));
            case "killEnemyTotal":
                return new KillAllEnemyTotalCountQuest(int.Parse(s[1]));
            case "recordBestScore":
                return new BestScoreQuest(int.Parse(s[1]));
            default:
                return null;
        }

    }

}
