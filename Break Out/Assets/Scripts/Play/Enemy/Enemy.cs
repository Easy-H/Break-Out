using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    public void Start () {
        EnemyManager.CreateCount++;
    }

    public void Destroy(string killedBy)
    {
        switch (killedBy) {
            case "Gong":
                EnemyManager.DestroyByGongCount++;
                break;
            default:
                EnemyManager.DestroyByLaserCount++;
                break;
        }

        Destroy(gameObject);
    }

}
