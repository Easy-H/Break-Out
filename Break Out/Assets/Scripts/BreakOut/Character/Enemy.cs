using UnityEngine;

public class Enemy : Character, IBallTarget {

    [SerializeField] private string _name;
    [SerializeField] private int _score = 100;

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

        _status.SetHPMax();
        base.DieAct();
    }
}
