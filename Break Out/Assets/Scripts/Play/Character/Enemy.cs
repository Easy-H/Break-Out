using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IBallTarget {

    [SerializeField] string _name;
    [SerializeField] int _score = 100;

    public void BallCollideAction()
    {
        SoundManager.Instance.PlayEffect("Collide_Enemy");
        GetDamaged(1);
    }

    protected override void DieAct()
    {
        StatisticsManager.Instance.AddKillData(_name);
        GameManager.Instance.AddScore(_score);
        base.DieAct();
        _stat.SetHPMax();
    }
}
