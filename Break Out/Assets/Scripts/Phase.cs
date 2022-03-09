using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    
    [SerializeField] float phaseEndScore = 0;
    [SerializeField] int endEnemyKill = 0;
    [SerializeField] int  killedEnemy = 0;

    public int maxEnemy;

    public bool PhaseEndCheck(float endScore) {
        if (endScore >= phaseEndScore)
            return true;
        else
            return false;
    }

    public bool PhaseEndCheck()
    {
        killedEnemy++;
        if (killedEnemy >= endEnemyKill) {
            return true;
        }
        else
            return false;
    }
    
}
