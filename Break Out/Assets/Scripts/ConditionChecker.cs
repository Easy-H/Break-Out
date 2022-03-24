using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionContents {
    public string name;
    public string value;
}

[System.Serializable]
public class ConditionChecker
{
    [SerializeField] ConditionContents[] conditions = null;

    public bool ConditionCheck() {
        bool result = true;

        if (conditions == null)
            return false;

        for (int i = 0; i < conditions.Length && result; i++) {
            switch (conditions[i].name) {
                case "EnemyKill":
                    result = CheckEnemyKill(int.Parse(conditions[i].value));
                    break;
                case "Score":
                    result = CheckScore(float.Parse(conditions[i].value));
                    break;
                default:
                    break;

            }
        }

        return result;
    }


    public bool CheckScore(float endScore)
    {
        if (GameManager.instance.Score >= endScore)
            return true;
        else
            return false;
    }

    public bool CheckEnemyKill(int maxKill)
    {
        if (EnemyManager.DestroyByGongCount >= maxKill)
            return true;
        else
            return false;
    }
}
