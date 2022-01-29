using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : Creator
{

    protected override void AddTime()
    {
        if (!GameManager.instance.CheckCreateEnemy())
            return;

        base.AddTime();
    }

    // Update is called once per frame
    protected override void Create()
    {

        base.Create();

        GameManager.enemyCount += goalCreateCount;
    }
}
