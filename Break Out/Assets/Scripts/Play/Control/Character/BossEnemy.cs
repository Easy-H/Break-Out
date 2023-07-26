using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    protected override void DieAct()
    {
        GameManager.Instance.BossKill();
        GameManager.Instance.AddScore(1000);
        base.DieAct();
        Destroy(gameObject);
    }
}
