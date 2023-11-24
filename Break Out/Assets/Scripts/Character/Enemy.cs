using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    [SerializeField] string _name;

    protected override void DieAct()
    {
        StatisticsManager.Instance.AddKillData(_name);
        GameManager.Instance.AddScore(100);
        base.DieAct();
        _stat.SetHPMax();
    }
}
