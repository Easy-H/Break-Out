using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IBallTarget {

    [SerializeField] string _name;
    [SerializeField] int _score = 100;

    Pool _parent;

    public void BallCollideAction()
    {
        SoundManager.Instance.PlayEffect("Collide_Enemy");
        GetDamaged(1);
    }

    protected override void DieAct()
    {
        StatisticsManager.Instance.AddKillData(_name);
        GameManager.Instance.Playground.AddScore(_score);
        SoundManager.Instance.PlayEffect("KillEnemy");
        Effect.PlayEffect("Eft_Pop", transform.position);

        _stat.SetHPMax();
        base.DieAct();
    }
}
