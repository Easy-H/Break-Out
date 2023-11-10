using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    protected override void DieAct()
    {
        GameManager.Instance.EnemyKill();
        GameManager.Instance.AddScore(100);
        base.DieAct();
        Destroy(gameObject);
    }
}
