using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : Creator
{

    protected override void AddTime()
    {
        if (!PhaseManager.instance.CheckCreateEnemy())
            return;

        base.AddTime();
    }

    // Update is called once per frame
    protected override void Create()
    {
        base.Create();
    }
}
