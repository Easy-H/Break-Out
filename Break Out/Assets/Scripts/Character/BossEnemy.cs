using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{

    protected override void DieAct()
    {
        base.DieAct();
        Destroy(gameObject);
    }
}
