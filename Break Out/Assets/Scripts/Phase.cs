using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Phase: MonoBehaviour
{
    [SerializeField] ConditionChecker conditionCheck = null;
    public int maxEnemy;

    public bool EndCheck() {
        return conditionCheck.ConditionCheck();
    }


    
}
